using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Meta.WitAi.Events;
using System;
using TMPro;

namespace Meta.WitAi.Dictation
{
    public class TranscriptionHandler : MonoBehaviour
    {
        [SerializeField] private DictationService witDictation;
        [SerializeField] private int linesBetweenActivations = 2;
         private TMP_Text textbox;
        [Multiline]
        [SerializeField] private string activationSeparator = String.Empty;

        [Header("Events")]
        [SerializeField]
        private WitTranscriptionEvent onTranscriptionUpdated = new
            WitTranscriptionEvent();

        private static StringBuilder _text;
        private string _activeText;
        private bool _newSection;

        private StringBuilder _separator;

        private void Awake()
        {
            if (!witDictation) witDictation = FindObjectOfType<DictationService>();

            textbox = this.GetComponent<TMP_Text>();
            _text = new StringBuilder();
            _separator = new StringBuilder();
            for (int i = 0; i < linesBetweenActivations; i++)
            {
                _separator.AppendLine();
            }

            if (!string.IsNullOrEmpty(activationSeparator))
            {
                _separator.Append(activationSeparator);
            }
        }
       
        private void OnEnable()
        {
            witDictation.DictationEvents.OnFullTranscription.AddListener(OnFullTranscription);
            witDictation.DictationEvents.OnPartialTranscription.AddListener(OnPartialTranscription);
            witDictation.DictationEvents.OnAborting.AddListener(OnCancelled);
        }

        private void OnDisable()
        {
            _activeText = string.Empty;
            witDictation.DictationEvents.OnFullTranscription.RemoveListener(OnFullTranscription);
            witDictation.DictationEvents.OnPartialTranscription.RemoveListener(OnPartialTranscription);
            witDictation.DictationEvents.OnAborting.RemoveListener(OnCancelled);
        }

        private void OnCancelled()
        {
            _activeText = string.Empty;
            textbox.text = _text.ToString();
            OnTranscriptionUpdated();
        }

        private void OnFullTranscription(string text)
        {
            _activeText = string.Empty;

            if (_text.Length > 0)
            {
                _text.Append(_separator);
            }

            _text.Append(text);
            textbox.text = _text.ToString();
            OnTranscriptionUpdated();
        }

        private void OnPartialTranscription(string text)
        {
            _activeText = text;
            OnTranscriptionUpdated();
        }

        public void Clear()
        {
            _text.Clear();
            textbox.text = _text.ToString();
            onTranscriptionUpdated.Invoke(string.Empty);
        }

        private void OnTranscriptionUpdated()
        {
            var transcription = new StringBuilder();
            transcription.Append(_text);
            if (!string.IsNullOrEmpty(_activeText))
            {
                if (transcription.Length > 0)
                {
                    transcription.Append(_separator);
                }

                if (!string.IsNullOrEmpty(_activeText))
                {
                    transcription.Append(_activeText);
                }
            }
            onTranscriptionUpdated.Invoke(transcription.ToString());
            
        }
    }
}
