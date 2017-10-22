using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Put this script on an empty special object on the Scene.
/// </summary>
public class TouchController : MonoBehaviour
{
#region Delegates-propertys
    public delegate void EventHandlerTouches(Vector3 pos);
    public delegate void EventHandlerTouchesMove(Vector3 pos, Vector3 delta);

    public static event EventHandlerTouches OnTouchDown;
    public static event EventHandlerTouches OnTouchUp;
    public static event EventHandlerTouchesMove OnTouchMove;
#endregion

#region Creating Events
    public static void TouchDown(Vector3 pos)
    {
        if (OnTouchDown != null)
        {
            OnTouchDown(pos);
        }
    }

    public static void TouchUp(Vector3 pos)
    {
        if (OnTouchUp != null)
        {
            OnTouchUp(pos);
        }
    }

    public static void TouchMove(Vector3 pos, Vector3 delta)
    {
        if (OnTouchMove != null)
        {
            OnTouchMove(pos, delta);
        }
    }
#endregion

#region Functional methods
    /// <summary>
    /// Functional methods.
    /// Getting Inputs info
    /// </summary>

#if UNITY_EDITOR || UNITY_WINDOWS
    private bool isTouch = false;
    private Vector3 lastTouch;
#endif
    void Update()
    {
#if UNITY_EDITOR || UNITY_WINDOWS
        
        // Call event OnTouchDown from mouse
        if (Input.GetMouseButtonDown(0))
        {
            isTouch = true;
            if (OnTouchDown != null) TouchController.OnTouchDown(Input.mousePosition);
            lastTouch = Input.mousePosition;
        }

        // Call event OnTouchMove
        if (isTouch)
        {
            if (OnTouchMove != null) TouchController.OnTouchMove(Input.mousePosition, Input.mousePosition - lastTouch);
            lastTouch = Input.mousePosition;
        }

        // Call event OnTouchUp from mouse
        if (Input.GetMouseButtonUp(0))
        {
            isTouch = false;
            if (OnTouchUp != null) TouchController.OnTouchUp(Input.mousePosition);
        }
#else
                
        if (Input.touchCount > 0)
        {
            switch(Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:
                    if (OnTouchDown != null) TouchController.OnTouchDown(Input.GetTouch(0).position);
                    break;
                case TouchPhase.Moved:
                    if (OnTouchMove != null) TouchController.OnTouchMove(Input.GetTouch(0).position, Input.GetTouch(0).deltaPosition);
                    break;
                case TouchPhase.Ended:
                    if (OnTouchUp != null) TouchController.OnTouchUp(Input.GetTouch(0).position);
                    break;
                default:
                    break;
            }
        }
#endif
    }
#endregion
}
