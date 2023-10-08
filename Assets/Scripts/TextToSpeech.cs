using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Meta.WitAi.TTS.Utilities;
using TMPro;

public class TextToSpeech : MonoBehaviour
{
    // Speaker
    [SerializeField] private TTSSpeaker _speaker;

    [SerializeField] private TMP_Text _input;

    private bool _speaking;

    public void Speak()
    {
        // Speak phrase
        _speaker.Speak(_input.text);
    }
    private void Update()
    {
        _speaking = _speaker.IsSpeaking;
    }

    public bool Speaking()
    {
        return _speaking;
    }
}
