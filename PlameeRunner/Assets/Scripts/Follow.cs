using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {
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
}
