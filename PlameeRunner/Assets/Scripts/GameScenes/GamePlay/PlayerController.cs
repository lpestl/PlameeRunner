using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public bool showInfo = true;
    public float maxSpeedX = 50.0f;

    //private Vector3 currentSpeed = Vector3.zero;
    public Vector3 acceleration = new Vector3(10.0f, 0.0f, 0.0f);
    public Vector3 jumpForce = new Vector3(0.0f, 10.0f, 0.0f);
    public Vector3 gravityVector = new Vector3(0.0f, -9.8f, 0.0f);

    public LayerMask groundMask;

    private bool underfoot = false;
    private Rigidbody rigidbodyPlayer;
    private Collider colliderPlayer;

    // Use this for initialization
    void Start () {
        Physics.gravity = gravityVector;
        rigidbodyPlayer = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        rigidbodyPlayer.velocity = new Vector3(rigidbodyPlayer.velocity.x + acceleration.x * Time.deltaTime,
                                               rigidbodyPlayer.velocity.y + acceleration.y * Time.deltaTime,
                                               rigidbodyPlayer.velocity.z + acceleration.z * Time.deltaTime);
        
        if (rigidbodyPlayer.velocity.x >= maxSpeedX)
        {
            acceleration = Vector3.zero;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground")) 
        {
            Debug.Log("Touch groynd");
        }            
    }

    private void OnGUI()
    {
        if (showInfo)
        {
            GUI.Label(new Rect(25, 25, Screen.width - 50, 25), "MaxSpeedX = " + maxSpeedX.ToString());
            GUI.Label(new Rect(25, 70, Screen.width - 50, 25), "Speed = " + rigidbodyPlayer.velocity.ToString());
        }
    }

    private void OnEnable()
    {
        TouchController.OnTouchDown += OnTap;
    }

    private void OnDisable()
    {
        TouchController.OnTouchDown -= OnTap;
    }

    private void OnTap(Vector3 pos)
    {
        rigidbodyPlayer.velocity = new Vector3(rigidbodyPlayer.velocity.x + jumpForce.x, rigidbodyPlayer.velocity.y + jumpForce.y, rigidbodyPlayer.velocity.z + jumpForce.z);
    }
}
