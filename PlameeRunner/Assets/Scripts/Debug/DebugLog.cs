using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Just temporary work code for verification.
/// ATTENTION!!!
/// Do not look here even in the face of death.Blood can go from the eyes !!!
/// </summary>
public class DebugLog : MonoBehaviour {
    
    public static DebugLog instance;

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
    
    public List<string> messages;

    void Start()
    {
        messages = new List<string>();
        windowRect = new Rect(Screen.width - (float) Screen.width / 4.0f , 10, Screen.width / 4.0f - 10, Screen.height - 20);
    }
    
    void OnEnable()
    {
        TouchController.OnTouchDown += TouchController_OnTouchDown;
        TouchController.OnTouchUp += TouchController_OnTouchUp;
        TouchController.OnTouchMove += TouchController_OnTouchMove;
    }

    private void TouchController_OnTouchMove(Vector3 pos, Vector3 delta)
    {
        //Print("Pos: " + pos + "; Delta: " + delta);
    }

    private void TouchController_OnTouchUp(Vector3 pos)
    {
        Print("Touch Up: " + pos);
    }

    private void TouchController_OnTouchDown(Vector3 pos)
    {
        Print("Touch Down: " + pos);
    }

    void OnDisable()
    {
        TouchController.OnTouchDown -= TouchController_OnTouchDown;
        TouchController.OnTouchUp -= TouchController_OnTouchUp;
        TouchController.OnTouchMove -= TouchController_OnTouchMove;
    }

    public void Print(string msg)
    {
        messages.Add(msg);
        Debug.Log(msg);
    }

    private Rect windowRect;
    void OnGUI()
    {
        windowRect = GUI.Window(1, windowRect, GuiConsole, "GUI log");
    }

    private void GuiConsole(int id)
    {
        GUI.Label(new Rect(25, 15, windowRect.width - 50, 25), "Count = " + messages.Count);
        int y = 15, deltay = 20;
        for (var i = messages.Count -1; i >0; i--)
        {
            GUI.Label(new Rect(10, y += deltay, windowRect.width - 20, windowRect.height - 30), messages[i]);
        }

        if (messages.Count > 100)
        {
            messages.RemoveAt(0);
        }
    }
    
}
