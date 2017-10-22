using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {

    public float durationTime = 0.5f;
    
    public void FadeIn()
    {
        StartCoroutine(FadeTo(1.0f));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeTo(0.0f));
    }

    IEnumerator FadeTo(float alpha)
    {
        var startAlpha = GetComponent<CanvasGroup>().alpha;
        for (var dt = 0.0f; dt <= 1.0; dt += Time.deltaTime / durationTime)
        {
            Debug.Log(GetComponent<CanvasGroup>().alpha);
            GetComponent<CanvasGroup>().alpha = Mathf.Lerp(startAlpha, alpha, dt);
            yield return null;
        }
        GetComponent<CanvasGroup>().alpha = alpha;
        yield return null;
    }
}
