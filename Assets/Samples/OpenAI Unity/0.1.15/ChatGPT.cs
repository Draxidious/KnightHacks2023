using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Threading.Tasks;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

namespace OpenAI
{
    public class ChatGPT : MonoBehaviour
    {
        [SerializeField] public Text inputField;

        UnityEvent m_MyEvent = new UnityEvent();

        private List<ChatMessage> _msg = new List<ChatMessage>();
        
        private string _aiInput = "You are an interviewer for the job role of Networking Engineer at your company. You are interviewing an individual for the position of Networking Engineer. You are to act professionally and converse with your interviewee, and ask questions to assess your interviewee's behavioral and technical proficiency in the interest of hiring them. It's in your best interest to learn about as many relevant aspects about an individual's qualifications in as little questions as possible. Do not list questions, the interview is meant to be a friendly conversation. You are meant to ask one or two relevant questions at a time. You are to aptly end the meeting if the interviewee is sufficiently disrespectful, crass, insubordinate, or unprofessional to a harassing degree. Interviews should last from 3 to 5 questions, and should always end by thanking the interviewee for their time. The end of the interview should always end with the phrase \"Have a fantastic day!\"";
        private OpenAIApi _openAi = new OpenAIApi("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");

        // Start is called before the first frame update
        void Start()
        {
            inputField.text = "yolo swag";
            m_MyEvent.AddListener(SendReply);

            if (true)
            {
                m_MyEvent.Invoke();
            }
        }

        private async void SendReply()
        {
            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = this._aiInput
            };

            this._msg.Add(newMessage);

            var completionResponse = await this._openAi.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-3.5-turbo-0613",
                Messages = this._msg,
                MaxTokens = 128
            });

            var message = completionResponse.Choices[0].Message;
            message.Content = message.Content.Trim();

            inputField.text = message.Content;
        }
    }
}
