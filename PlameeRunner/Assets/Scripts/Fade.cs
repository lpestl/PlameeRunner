using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {
    public delegate void EventHandlerFadeEnded();

    public static EventHandlerFadeEnded OnFadeEnded;

    public static void FadeEnded()
    {
        if (OnFadeEnded != null)
        {
            OnFadeEnded();
        }
    }

    public float durationTime = 0.5f;
    
    public void FadeIn()
    {
        StartCoroutine(FadeTo(1.0f));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeTo(0.0f));
    }

    public IEnumerator FadeTo(float alpha)
    {
        var startAlpha = GetComponent<CanvasGroup>().alpha;
        for (var dt = 0.0f; dt <= 1.0; dt += Time.deltaTime / durationTime)
        {
            GetComponent<CanvasGroup>().alpha = Mathf.Lerp(startAlpha, alpha, dt);
            yield return null;
        }
        GetComponent<CanvasGroup>().alpha = alpha;

        if (alpha == 0.0f) gameObject.SetActive(false);
        yield return null;
        Fade.FadeEnded();
    }
}
