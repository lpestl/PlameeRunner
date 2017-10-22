using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
#region Properties
    public Fade fade;
    public float durationFade = 1.0f;

    #endregion

    #region Init manager
    void Start()
    {
        FadeOut();
    }
#endregion

#region Subscribe
    private void OnEnable()
    {
        GameEventHandlers.OnFadeIn += FadeIn;
        GameEventHandlers.OnFadeOut += FadeOut;
    }

    private void OnDisable()
    {
        GameEventHandlers.OnFadeIn -= FadeIn;
        GameEventHandlers.OnFadeOut -= FadeOut;
    }

    public void FadeIn()
    {
        fade.gameObject.SetActive(true);
        StartCoroutine(fade.FadeTo(1.0f, durationFade));
    }

    public void FadeOut()
    {
        if (fade.isActiveAndEnabled)
        {
            StartCoroutine(fade.FadeTo(0.0f, durationFade));
        }
    }
#endregion
}
