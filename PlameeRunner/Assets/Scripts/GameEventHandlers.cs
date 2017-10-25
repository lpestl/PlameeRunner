using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventHandlers : MonoBehaviour {
#region Delegates
    public delegate void EventHandlerFade();

    public static EventHandlerFade OnFadeIn;
    public static EventHandlerFade OnFadeOut;
    public static EventHandlerFade OnFadeEnded;
#endregion

#region Static call event
    public static void FadeIn()
    {
        if (OnFadeIn != null)
        {
            OnFadeIn();
        }
    }

    public static void FadeOut()
    {
        if (OnFadeOut != null)
        {
            OnFadeOut();
        }
    }

    public static void FadeEnded()
    {
        if (OnFadeEnded != null)
        {
            OnFadeEnded();
        }
    }
#endregion
}
