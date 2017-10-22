using StateStuff;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {
#region Properties-screens
    public RectTransform mainMenuScreen;
    public RectTransform optionsScreen;
#endregion

#region Implementing menu screens
    public enum MenuScreen { MAINMENU, OPTIONS }
    public MenuScreen currentScreen;

    void Start()
    {
        switch (currentScreen)
        {
            case MenuScreen.MAINMENU:
                ShowMainMenu();
                break;
            case MenuScreen.OPTIONS:
                ShowOptions();
                break;
            default:
                break;
        }
    }

    public void ShowOptions()
    {
        mainMenuScreen.gameObject.SetActive(false);
        optionsScreen.gameObject.SetActive(true);
    }

    public void ShowMainMenu()
    {
        mainMenuScreen.gameObject.SetActive(true);
        optionsScreen.gameObject.SetActive(false);
    }
#endregion

#region Buttons delegates
    public delegate void EventHandlerClickButton();
    public static EventHandlerClickButton OnClickStart;
    public static EventHandlerClickButton OnClickExit;

    public void ClickStart()
    {
        if (OnClickStart != null)
        {
            OnClickStart();
        }
    }

    public void ClickExit()
    {
        if (OnClickExit != null)
        {
            OnClickExit();
        }
    }
#endregion
}
