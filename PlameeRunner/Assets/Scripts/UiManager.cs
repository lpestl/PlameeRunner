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

    void OnEnable()
    {
        TouchController.OnTouchDown += TouchController_OnTouchDown;
    }

    private int countTouch = 1;
    private void TouchController_OnTouchDown(Vector3 pos)
    {
        switch (countTouch % 3)
        {
            case 0:
                GameSceneManager.instance.ChangeSceneWithFade(GameSceneManager.GameScene.MENU);
                break;
            case 1:
                GameSceneManager.instance.ChangeSceneWithFade(GameSceneManager.GameScene.CHOICE_CHAREPTER);
                break;
            case 2:
                GameSceneManager.instance.ChangeSceneWithFade(GameSceneManager.GameScene.GAMEPLAY);
                break;
            default:
                break;
        }
        countTouch++;
        
    }

    void OnDisable()
    {
        TouchController.OnTouchDown -= TouchController_OnTouchDown;
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
