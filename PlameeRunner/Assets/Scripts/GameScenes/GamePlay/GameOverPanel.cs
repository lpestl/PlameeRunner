using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour {
#region control panel
    public GameObject panelGameOver;

    private void Start()
    {
        panelGameOver.SetActive(false);
    }
    #endregion

#region Buttons callbacks
    public void OnRetry()
    {
        LevelEventSystem.RetryLevel();
    }

    public void OnBackMenu()
    {
        LevelEventSystem.BackMenu();
    }
#endregion

#region Subscribed on game over event
    private void OnEnable()
    {
        LevelEventSystem.OnGameOver += GameOverShow;
    }

    private void OnDisable()
    {
        LevelEventSystem.OnGameOver -= GameOverShow;
    }

    private void GameOverShow()
    {
        panelGameOver.SetActive(true);
    }
#endregion
}
