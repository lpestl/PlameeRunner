using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceUi : MonoBehaviour {
    public RectTransform targetCharapter;
    public RectTransform targetLocation;

    public RectTransform textCharapter;
    public RectTransform textWorld;

    public List<RectTransform> charapterButtons;
    public List<RectTransform> worldButtons;

	// Use this for initialization
	void Start () {
        var indexCharapter = PlayerPrefs.GetInt("Charapter", 0);
        var indexWorld = PlayerPrefs.GetInt("World", 0);
        
        TargetOnCharapter(indexCharapter);
        TargetOnWorld(indexWorld);

        targetCharapter.gameObject.SetActive(false);
        targetLocation.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClickCube()
    {
        Debug.Log("Cube");
        TargetOnCharapter(0);
    }

    public void OnClickSphere()
    {
        Debug.Log("Spher");
        TargetOnCharapter(1);
    }

    public void OnClickTriangle()
    {
        Debug.Log("Triangle");
        TargetOnCharapter(2);
    }

    void TargetOnCharapter(int index)
    {
        targetCharapter.position = charapterButtons[index].position;
        targetCharapter.gameObject.SetActive(true);
        PlayerPrefs.SetInt("Charapter", index);
        
        switch(index)
        {
            case 0:
                textCharapter.GetComponent<UnityEngine.UI.Text>().text = "Квадрат";
                break;
            case 1:
                textCharapter.GetComponent<UnityEngine.UI.Text>().text = "Круг";
                break;
            case 2:
                textCharapter.GetComponent<UnityEngine.UI.Text>().text = "Треугольник";
                break;
            default:
                textCharapter.GetComponent<UnityEngine.UI.Text>().text = "NONE";
                break;
        }
    }

    public void OnClickWorld01()
    {
        Debug.Log("World01");
        TargetOnWorld(0);
    }

    public void OnClickWorld02()
    {
        Debug.Log("World02");
        TargetOnWorld(1);
    }

    public void OnClickWorld03()
    {
        Debug.Log("World03");
        TargetOnWorld(2);
    }

    void TargetOnWorld(int index)
    {
        targetLocation.position = worldButtons[index].position;
        targetLocation.gameObject.SetActive(true);
        PlayerPrefs.SetInt("World", index);

        switch (index)
        {
            case 0:
                textWorld.GetComponent<UnityEngine.UI.Text>().text = "На плац";
                break;
            case 1:
                textWorld.GetComponent<UnityEngine.UI.Text>().text = "Танковая стоянка";
                break;
            case 2:
                textWorld.GetComponent<UnityEngine.UI.Text>().text = "В лес";
                break;
            default:
                textWorld.GetComponent<UnityEngine.UI.Text>().text = "NONE";
                break;
        }
    }

    public void OnClickPlay()
    {
        Debug.Log("Play");
        PlayInWorld();
    }

    public delegate void EventHandlerClickButton();
    public static EventHandlerClickButton OnClickPlayInWorld;

    public void PlayInWorld()
    {
        if (OnClickPlayInWorld != null)
        {
            OnClickPlayInWorld();
        }
    }
}
