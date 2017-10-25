using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {
    // NOTE: Not very good implementation of following the camera, but in any case, it's simple and works.
#region Easy Crutch 
    private Transform target;
	private Vector3 deltaPos;

    // Use this for initialization
	void Start () {
        target = Camera.main.transform;
        deltaPos = target.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = target.position - deltaPos;
	}
#endregion
}
