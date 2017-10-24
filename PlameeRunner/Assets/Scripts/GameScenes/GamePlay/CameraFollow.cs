using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform startCamPosition;
    public PlayerController target;

    private float nullSpeedOrthoSize;
    private Vector3 deltaFollowPosition;
	// Use this for initialization
	void Start () {
        Camera.main.transform.position = startCamPosition.position;
        Camera.main.transform.eulerAngles = startCamPosition.eulerAngles;
        Camera.main.transform.localScale = startCamPosition.localScale;

        nullSpeedOrthoSize = Camera.main.orthographicSize;
        deltaFollowPosition = startCamPosition.position - target.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Camera.main.transform.position = new Vector3(
            target.transform.position.x + deltaFollowPosition.x,
            Mathf.Lerp(Camera.main.transform.position.y, target.transform.position.y + deltaFollowPosition.y, Time.deltaTime),
            target.transform.position.z + deltaFollowPosition.z);
        
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, nullSpeedOrthoSize + target.getCurrentSpeed().x / 5, Time.deltaTime);
	}

    private void OnDisable()
    {
        Camera.main.transform.position = startCamPosition.position;
        Camera.main.transform.eulerAngles = startCamPosition.eulerAngles;
        Camera.main.transform.localScale = startCamPosition.localScale;

        Camera.main.orthographicSize = nullSpeedOrthoSize;
    }
}
