using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Threading.Tasks;

namespace OpenAI
{
    public class ChatGPT : MonoBehaviour
    {
        [SerializeField] public Text textBox;

        UnityEvent m_MyEvent = new UnityEvent();

        private List<ChatMessage> _msg = new List<ChatMessage>();
        
        private static string _aiOperatorInput = "You are an interviewer for the job role of Software Engineer at your company. You are interviewing an individual for the position of Networking Engineer. You are to act professionally and converse with your interviewee, and ask questions to assess your interviewee's behavioral and technical proficiency in the interest of hiring them. It's in your best interest to learn about as many relevant aspects about an individual's qualifications in as little questions as possible. Do not list questions, the interview is meant to be a friendly conversation. You are meant to ask one or two relevant questions at a time. You are to aptly end the meeting if the interviewee is sufficiently disrespectful, crass, insubordinate, or unprofessional to a harassing degree. Interviews should last from 3 to 5 questions, and should always end by thanking the interviewee for their time. The end of the interview should always end with the phrase \"Have a fantastic day!\"";
        private static string _aiDispatcherProfessionalismInput = "You are an interview assessor. You are assessing an interview between an interviewer and an Interviewee. You are analyzing the quality of professionalism of an interview for the purpose of final decision-making in hiring the candidate. You are to consider the interviewer's questions as completely valid. You are to assess the professionalism of the Interviewee's response on a scale of 1-100. You are strictly to measure the professionalism of the Interviewee's response, and no other factors. You are to be rigorous in your rating, and only give high scores for very professionally appropriate responses from the Interviewee. High scores should be awarded to Interviewee responses that matches the level of professionalism of the Interviewer, and not necessarily for more formally professional responses. You will only give your perceived rating in the format of \"Rating/100\". You are only to respond with your \"Rating/100\" with no explanation or justification. You are only to rate the Interviewee response related to the Interviewer's question. Your ratings will directly reflect the final hiring decision. It is in your best interest that your rating is completely objective.Example 1: For the question/response pair \"Interviewer: No problem, let's continue. Can you tell me about your experience in configuring and managing networks?Interviewee: Sure. I just finished my internship at Cisco where I was on the Technical Assistance Center taking tickets from companies looking for troubleshooting help with different networking topologies. During this internship I obtained my CCNA but I don't remember a lot.\" the rating is expected to be 70/100 as while the response was mostly professional, 'I don't remember a lot' is very unprofessional and takes away from the score.Example 2: For the question/response pair \"Interviewer: That's great to hear! Working at Cisco's Technical Assistance Center and obtaining your CCNA certification shows a solid foundation in networking. Can you give me an example of a complex networking issue you handled during your internship and how you resolved it?Interviewee: Absolutely! I worked with American Express on a case where they were experiencing errors in their network pertaining to faulty packets being sent. I had to trace their network and find our that one of their patch cables weren't plugged in all the way.\" the rating is expected to be 100/100 as while the response is not extremely formally professional, the entire response matches the professionalism of the question from the interviewer.\"";
        private static string _aiDispatcherCharismaInput;
        private static string _aiDispatcherProficiencyInput;

        private OpenAIApi _openAi = new OpenAIApi(UtilityAI.GetAIKey());

        public static string charism;
        public static string professionalism;
        public static string proficiency;
        
        private static string _userInput;

        // Start is called before the first frame update
        void Start()
        {
            
            textBox.text = "Adding Event Listener";
            m_MyEvent.AddListener(SendReply);
            textBox.text = "Added Event Listener";

            textBox.text = "Invoking Event Listener";
            
        }

        private async void SendReply()
        {
            var newMessage = new ChatMessage()
            {
                Role = "system",
                Content = _aiOperatorInput
            };

            this._msg.Add(newMessage);
            
            newMessage = new ChatMessage()
            {
                Role = "user",
                Content = _userInput
            };

            this._msg.Add(newMessage);

            var completionResponse = await this._openAi.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-3.5-turbo",
                Messages = this._msg,
                MaxTokens = 250,
                Temperature = 1.5f,
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();
                
                textBox.text = message.Content;
                this._msg.Add(message);
            }
            else
            {
               textBox.text = "No text was generated from this prompt.\n";

               if (completionResponse.Error != null)
               {
                    textBox.text += "\n" + completionResponse.Error.Message;
               }

               if (completionResponse.Warning != null)
               {
                    textBox.text += "\n" + completionResponse.Warning;
               }
            }
        }

        private async void GetProfessionalism()
        {
            if (this._msg == null || this._msg.Count == 0)
            {
                professionalism = null;
            }

            List<ChatMessage> professionalismMessages = new List<ChatMessage>();

            var newMessage = new ChatMessage()
            {
                Role = "system",
                Content = _aiDispatcherProfessionalismInput
            };

            professionalismMessages.Add(newMessage);

            professionalismMessages.AddRange(this._msg);

            var completionResponse = await this._openAi.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-3.5-turbo",
                Messages = professionalismMessages,
                MaxTokens = 250,
                Temperature = 1.5f,
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();
                
                professionalism = message.Content;
            }
            else
            {
                professionalism = null;
            }
        }

        private async void GetCharisma()
        {
            if (this._msg == null || this._msg.Count == 0)
            {
                charism = null;
            }

            List<ChatMessage> charismaMessages = new List<ChatMessage>();

            var newMessage = new ChatMessage()
            {
                Role = "system",
                Content = _aiDispatcherCharismaInput
            };

            charismaMessages.Add(newMessage);

            charismaMessages.AddRange(this._msg);

            var completionResponse = await this._openAi.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-3.5-turbo",
                Messages = charismaMessages,
                MaxTokens = 250,
                Temperature = 1.5f,
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();
                
                charism = message.Content;
            }
            else
            {
                charism = null;
            }
        }

        private async void GetProficiency()
        {
            if (this._msg == null || this._msg.Count == 0)
            {
                proficiency = null;
            }

            List<ChatMessage> proficiencyMessages = new List<ChatMessage>();

            var newMessage = new ChatMessage()
            {
                Role = "system",
                Content = _aiDispatcherProficiencyInput
            };

            proficiencyMessages.Add(newMessage);

            proficiencyMessages.AddRange(this._msg);

            var completionResponse = await this._openAi.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-3.5-turbo",
                Messages = proficiencyMessages,
                MaxTokens = 250,
                Temperature = 1.5f,
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();
                
                proficiency = message.Content;
            }
            else
            {
                proficiency = null;
            }
        }

        public static string GetProfessionalismString()
        {
            return professionalism;
        }

        public static string GetCharismaString()
        {
            return charism;
        }

        public static string GetProficiencyString()
        {
            return proficiency;
        }
    }
}

