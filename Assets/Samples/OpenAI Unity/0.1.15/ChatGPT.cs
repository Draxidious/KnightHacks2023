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
        
        private string _aiOperatorInput = "You are an interviewer for the job role of Software Engineer at your company. You are interviewing an individual for the position of Networking Engineer. You are to act professionally and converse with your interviewee, and ask questions to assess your interviewee's behavioral and technical proficiency in the interest of hiring them. It's in your best interest to learn about as many relevant aspects about an individual's qualifications in as little questions as possible. Do not list questions, the interview is meant to be a friendly conversation. You are meant to ask one or two relevant questions at a time. You are to aptly end the meeting if the interviewee is sufficiently disrespectful, crass, insubordinate, or unprofessional to a harassing degree. Interviews should last from 3 to 5 questions, and should always end by thanking the interviewee for their time. The end of the interview should always end with the phrase \"Have a fantastic day!\"";
        private string _aiDispatcherInput = "You are an interview assessor. You are assessing an interview between an interviewer and an interviewee. You are analyzing the quality of an interview for the purpose of final decision-making in hiring the candidate. You are to consider the interviewer's questions as completely valid. You are to assess the interviewee's response on a scale of 1-100. The quality of an interviewee's response should be based on these criteria: 1. The appropriateness of the response. 2. The elaborative depth of the response. 3. The perceived qualification of the candidate for the role of Network Engineer. You are to be rigorous in your rating, and only give high scores for very strong responses from the interviewee. You will only give your perceived rating in the format of \"Rating/100\". You are only to respond with your \"Rating/100\" with no explanation or justification. You are only to rate the Interviewee response related to the Interviewer's question. Your ratings will directly reflect the final hiring decision. It is in your best interest that your rating is completely objective. Example 1: For the question/response pair \"Interviewer: No problem, let's continue. Can you tell me about your experience in configuring and managing networks? Interviewee: Sure. I just finished my internship at Cisco where I was on the Technical Assistance Center taking tickets from companies looking for troubleshooting help with different networking topologies. During this internship I obtained my CCNA but I don't remember a lot.\" the rating is expected to be 40/100 as while the response was professional and gave good information, the Interviewee insinuated that they don't remember a lot of the material from the CCNA exam, which is both unprofessional and extremely negative for our consideration of their qualification for the role. Example 2: For the question/response pair \"Interviewer: That's great to hear! Working at Cisco's Technical Assistance Center and obtaining your CCNA certification shows a solid foundation in networking. Can you give me an example of a complex networking issue you handled during your internship and how you resolved it? Interviewee: Absolutely! I worked with American Express on a case where they were experiencing errors in their network pertaining to faulty packets being sent. I had to trace their network and find our that one of their patch cables weren't plugged in all the way.\" the rating is expected to be 60/100 as while the response from the candidate was professional and relevant to the question, the response could have been more in-depth and elaborative on the nature of the issue and its solution.";
        private OpenAIApi _openAi = new OpenAIApi("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");

        // Start is called before the first frame update
        void Start()
        {
            string response;
            textBox.text = "Adding Event Listener";
            m_MyEvent.AddListener(GetRating);
            textBox.text = "Added Event Listener";

            textBox.text = "Invoking Event Listener";
            //response = m_MyEvent.Invoke("I hate this place. I wanna go home");
            textBox.text = response;
        }

        private async void SendReply()
        {
            var newMessage = new ChatMessage()
            {
                Role = "system",
                Content = this._aiOperatorInput
            };

            this._msg.Add(newMessage);
            
            newMessage = new ChatMessage()
            {
                Role = "user",
                Content = "Hello"
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

        private async Task<string> GetRating(string userResponse)
        {
            if (userResponse == null)
            {
                return null;
            }

            List<ChatMessage> ratingMessages = new List<ChatMessage>();

            var newMessage = new ChatMessage()
            {
                Role = "system",
                Content = this._aiDispatcherInput
            };

            ratingMessages.Add(newMessage);

            newMessage = new ChatMessage()
            {
                Role = "user",
                Content = userResponse
            };

            ratingMessages.Add(newMessage);

            var completionResponse = await this._openAi.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-3.5-turbo",
                Messages = ratingMessages,
                MaxTokens = 250,
                Temperature = 1.5f,
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();
                
                return message.Content;
            }
            else
            {
                return null;
            }
        }
    }
}
