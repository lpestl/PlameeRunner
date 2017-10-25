using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEventSystem : MonoBehaviour {
#region Delegates
    public delegate void EventHandlerVoid();

    public static EventHandlerVoid OnStartLevel;
    public static EventHandlerVoid OnGameOver;
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
    
#endregion
}
