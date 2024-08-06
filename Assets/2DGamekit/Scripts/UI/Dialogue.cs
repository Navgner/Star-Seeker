using UnityEngine;
using TMPro;
using System.Collections;

public class TextFollow : MonoBehaviour
{
    public Transform target; // The character or object the text should follow
    public float displayTime = 3f; // Time to display the text

    private TextMeshProUGUI textMeshPro;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        // Check if the CanvasGroup component is attached
        if (canvasGroup == null)
        {
            Debug.LogError("No CanvasGroup component found. Please add a CanvasGroup component to this GameObject.");
            return;
        }

        if (target == null)
        {
            Debug.LogError("Target not assigned!");
            return;
        }

        // Start coroutine to handle the display time
        StartCoroutine(DisplayText());
    }

    void Update()
    {
        // Update text position to follow the target
        if (target != null)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(target.position);
            rectTransform.position = screenPosition;
        }
    }

    private IEnumerator DisplayText()
    {
        // Ensure the text is visible
        canvasGroup.alpha = 1f;

        // Wait for the specified display time
        yield return new WaitForSeconds(displayTime);

        // Fade out text (optional) and then disable it
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, t);
            yield return null;
        }
        canvasGroup.alpha = 0;
    }
}
