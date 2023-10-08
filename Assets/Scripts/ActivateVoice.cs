using Meta.WitAi;
using Meta.WitAi.Requests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActivateVoice : MonoBehaviour
{
    // The button label to be adjusted with state
    [SerializeField] private TMP_Text _buttonLabel;

    [Tooltip("Text to be shown while the voice service is not active")]
    [SerializeField] private string _activateText = "Activate";

    [Tooltip("Reference to the current voice service")]
    [SerializeField] private VoiceService _voiceService;

    [Tooltip("Text to be shown while the voice service is active")]
    [SerializeField] private string _deactivateText = "Deactivate";

    private VoiceServiceRequest _request;
    void Awake()
    {
        if (_voiceService == null)
        {
            _voiceService = FindObjectOfType<VoiceService>();
        }
    }
    public void Activate()
    {
        _request = _voiceService.Activate(GetRequestEvents());
    }

    public void Deactivate()
    {
        _request.DeactivateAudio();
    }
    // Get events
    private VoiceServiceRequestEvents GetRequestEvents()
    {
        VoiceServiceRequestEvents events = new VoiceServiceRequestEvents();
        events.OnInit.AddListener(OnInit);
        events.OnComplete.AddListener(OnComplete);
        return events;
    }
    // Request initialized
    private void OnInit(VoiceServiceRequest request)
    {
        RefreshActive(true);
    }
    // Request completed
    private void OnComplete(VoiceServiceRequest request)
    {
        RefreshActive(false);
    }

    public void OnStoppedListeningDueToInactivity()
    {
        Activate();
    }

    // Refresh active text
    private void RefreshActive(bool _isActive)
    {
        if (_buttonLabel != null)
        {
            _buttonLabel.text = _isActive ? _deactivateText : _activateText;
        }
    }
}
