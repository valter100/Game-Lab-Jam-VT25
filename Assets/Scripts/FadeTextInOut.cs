using System.Collections;
using UnityEngine;
using TMPro;

public class FadeTextInOut : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float fadeDuration = 1.5f;
    public float visibleDuration = 1.0f;

    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = text.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = text.gameObject.AddComponent<CanvasGroup>();
        }
        StartCoroutine(FadeLoop());
    }

    IEnumerator FadeLoop()
    {
        while (true)
        {
            yield return StartCoroutine(Fade(0f, 1f, fadeDuration)); // Fade in
            yield return new WaitForSeconds(visibleDuration);
            yield return StartCoroutine(Fade(1f, 0f, fadeDuration)); // Fade out
        }
    }

    IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            yield return null;
        }
        canvasGroup.alpha = endAlpha;
    }
}
