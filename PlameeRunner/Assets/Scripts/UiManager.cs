using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour {
    public static UiManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    public Fade fade;

    void Start()
    {
        FadeOut();
    }

    public void FadeIn()
    {
        fade.gameObject.SetActive(true);
        fade.FadeIn();
    }

    public void FadeOut()
    {
        if (fade.isActiveAndEnabled)
        {
            fade.FadeOut();
        }
    }
}
