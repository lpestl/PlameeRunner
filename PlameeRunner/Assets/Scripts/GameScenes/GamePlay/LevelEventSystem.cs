using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEventSystem : MonoBehaviour {
#region Delegates
    public delegate void EventHandlerVoid();

    public static EventHandlerVoid OnStartLevel;
    public static EventHandlerVoid OnGameOver;

    public static EventHandlerVoid OnRetryLevel;
    public static EventHandlerVoid OnBackMenu;
#endregion

#region Static call event
    public static void StartLevel()
    {
        if (OnStartLevel != null)
        {
            OnStartLevel();
        }
    }

    public static void GameOver()
    {
        if (OnGameOver != null)
        {
            OnGameOver();
        }
    }
    
    public static void RetryLevel()
    {
        if (OnRetryLevel != null)
        {
            OnRetryLevel();
        }
    }

    public static void BackMenu()
    {
        if (OnBackMenu != null)
        {
            OnBackMenu();
        }
    }
#endregion
}
