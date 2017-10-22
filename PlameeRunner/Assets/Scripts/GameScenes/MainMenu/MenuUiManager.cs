using StateStuff;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUiManager : MonoBehaviour {
    public RectTransform mainMenu;
    public RectTransform Options;

    public enum MenuScreen { MAINMENU, OPTIONS }
    public MenuScreen currentScreen;

    void Start()
    {
        switch (currentScreen)
        {
            case MenuScreen.MAINMENU:
                ShowMainMenu();
                //mainMenu.gameObject.SetActive(true);
                break;
            case MenuScreen.OPTIONS:
                //Options.gameObject.SetActive(false);
                ShowOptions();
                break;
            default:
                break;
        }
    }

    public void ShowOptions()
    {
        Options.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
    }

    public void ShowMainMenu()
    {
        Options.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }

    public void ClickExit()
    {
        Application.Quit();
    }

    public delegate void EventHandlerClickStart();
    public static EventHandlerClickStart OnClickStart;

    public void ClickStart()
    {
        if (OnClickStart != null)
        {
            OnClickStart();
        }
    }
}
