using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {
#region Fade Implementing
    public IEnumerator FadeTo(float alpha, float duration)
    {
        var startAlpha = GetComponent<CanvasGroup>().alpha;
        for (var dt = 0.0f; dt <= 1.0; dt += Time.deltaTime / duration)
        {
            GetComponent<CanvasGroup>().alpha = Mathf.Lerp(startAlpha, alpha, dt);
            yield return null;
        }
        GetComponent<CanvasGroup>().alpha = alpha;

        GameEventHandlers.FadeEnded();
        if (alpha == 0.0f) gameObject.SetActive(false);
        yield return null;
    }
#endregion
}
