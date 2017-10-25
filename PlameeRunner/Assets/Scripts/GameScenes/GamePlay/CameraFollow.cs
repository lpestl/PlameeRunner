using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform startCamPosition;
    private GameObject target;

    private float nullSpeedOrthoSize;
    private Vector3 deltaFollowPosition;

	// Use this for initialization
	void Start () {
        Camera.main.transform.position = startCamPosition.position;
        Camera.main.transform.eulerAngles = startCamPosition.eulerAngles;
        Camera.main.transform.localScale = startCamPosition.localScale;
	}
	
    public void SetTarget(GameObject _o)
    {
        target = _o;
        CalculateDelta();
    }

    void CalculateDelta()
    {
        nullSpeedOrthoSize = Camera.main.orthographicSize;
        deltaFollowPosition = startCamPosition.position - target.transform.position;
    }

	// Update is called once per frame
	void Update () {
        if ((target != null) && (target.activeInHierarchy))
        {
            Camera.main.transform.position = new Vector3(
                target.transform.position.x + deltaFollowPosition.x,
                Mathf.Lerp(Camera.main.transform.position.y, target.transform.position.y + deltaFollowPosition.y, Time.deltaTime),
                target.transform.position.z + deltaFollowPosition.z);

            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, nullSpeedOrthoSize + target.GetComponent<PlayerController>().getCurrentSpeed().x / 5, Time.deltaTime);
        } else
        {
            Camera.main.transform.position = new Vector3(
                Camera.main.transform.position.x,
                Mathf.Lerp(Camera.main.transform.position.y, startCamPosition.position.y, Time.deltaTime),
                Camera.main.transform.position.z);
        }
	}

    private void OnEnable()
    {
        LevelEventSystem.OnGameOver += StopingCamera;
    }

    private void OnDisable()
    {
        LevelEventSystem.OnGameOver -= StopingCamera;
    }

    private void StopingCamera()
    {
        target = null;
    }
}
