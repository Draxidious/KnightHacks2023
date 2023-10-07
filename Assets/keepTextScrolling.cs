using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AutoScrollText : MonoBehaviour
{
    public TMP_Text textComponent;
    public ScrollRect scrollRect;

    private void Awake()
    {
        // Just a safety check to ensure both components are assigned.
        if (textComponent == null || scrollRect == null)
        {
            Debug.LogError("Please assign both the TextMeshPro component and the ScrollRect component in the inspector.");
            return;
        }
    }

    public void AddText(string newText)
    {
        textComponent.text += newText;

        // This is an important step.
        // It ensures the UI updates and calculates the new position of the content.
        Canvas.ForceUpdateCanvases();

        // Scroll to the bottom.
        scrollRect.verticalNormalizedPosition = 0.0f;
    }
}
