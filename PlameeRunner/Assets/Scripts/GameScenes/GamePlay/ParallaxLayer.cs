using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour {
    public float factorSpeed = 0.0f;

    private Vector3 lastPosCam;
    private Vector3 deltaPosCam;

    // Use this for initialization
    void Start () {
        lastPosCam = Camera.main.transform.position;	
	}
	
	// Update is called once per frame
	void LateUpdate () {
        deltaPosCam = Camera.main.transform.position - lastPosCam;
        lastPosCam = Camera.main.transform.position;

        transform.position = transform.position + deltaPosCam * factorSpeed;
    }
}
