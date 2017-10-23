using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public bool showInfo = true;
    public float maxSpeedX = 50.0f;

    //private Vector3 currentSpeed = Vector3.zero;
    public Vector3 acceleration = new Vector3(10.0f, 0.0f, 0.0f);
    public Vector3 gravityAccel = new Vector3(0.0f, -9.8f, 0.0f);
    private Vector3 currentAccel;
    public Vector3 jumpForce = new Vector3(0.0f, 10.0f, 0.0f);

    public LayerMask groundMask;

    public bool underfoot = false;
    private Rigidbody rigidbodyPlayer;

    //private Vector3 calculateSpeed = Vector3.zero;

    // Use this for initialization
    void Start () {
        rigidbodyPlayer = GetComponent<Rigidbody>();
        currentAccel = acceleration + gravityAccel;
    }
	
	// Update is called once per frame
	void Update () {
        //var lastPos = transform.position;
        // Rigidbody, I use only for the correct stinging and collision.
        // Speed for player's rigidbodi, we rely only on gravity.
        // The formula for the speed of acceleration is used: v = at
        rigidbodyPlayer.velocity = new Vector3(rigidbodyPlayer.velocity.x + currentAccel.x * Time.deltaTime,
                                               rigidbodyPlayer.velocity.y + currentAccel.y * Time.deltaTime,
                                               rigidbodyPlayer.velocity.z + currentAccel.z * Time.deltaTime);

        // In order not to take into account the friction of the surface, 
        // the displacement along X will be assumed proceeding from speed and acceleration.
        // The displacement formula: S = v0*t + a*t*t/2 
        //transform.position = new Vector3(transform.position.x + calculateSpeed.x * Time.deltaTime + currentAccel.x * Time.deltaTime * Time.deltaTime / 2,
        //                                 transform.position.y/* + calculateSpeed.y * Time.deltaTime + currentAccel.y * Time.deltaTime * Time.deltaTime / 2*/,
        //                                 transform.position.z/* + calculateSpeed.z * Time.deltaTime + currentAccel.z * Time.deltaTime * Time.deltaTime / 2*/);

        // Calculate Speed use formula: v = S / t
        //calculateSpeed = new Vector3(
        //    (transform.position.x - lastPos.x) / Time.deltaTime,
        //    (transform.position.y - lastPos.y) / Time.deltaTime,
        //    (transform.position.z - lastPos.z) / Time.deltaTime);

        //rigidbodyPlayer.velocity = calculateSpeed;

        if (rigidbodyPlayer.velocity.x >= maxSpeedX)
        {
            currentAccel = gravityAccel;
        } else
        {
            currentAccel = acceleration + gravityAccel;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision Enter");
        var collosionLayer = LayerMask.GetMask(LayerMask.LayerToName(collision.collider.gameObject.layer));
        if (collosionLayer == groundMask)
        {
            underfoot = true;

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //Debug.Log("Collision Exit");
        var collosionLayer = LayerMask.GetMask(LayerMask.LayerToName(collision.collider.gameObject.layer));
        if (collosionLayer == groundMask)
        {
            underfoot = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //Debug.Log("Collision Stay");
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            underfoot = true;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("Trigger enter");
    //    var collosionLayer = LayerMask.GetMask(LayerMask.LayerToName(other.gameObject.layer));
    //    if (collosionLayer == groundMask)
    //    {
    //        underfoot = true;
    //        calculateSpeed = new Vector3(calculateSpeed.x, 0, calculateSpeed.z);
    //        currentAccel = new Vector3(currentAccel.x, 0, currentAccel.z);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    Debug.Log("Trigger exit");
    //    var collosionLayer = LayerMask.GetMask(LayerMask.LayerToName(other.gameObject.layer));
    //    if (collosionLayer == groundMask)
    //    {
    //        underfoot = false;
    //    }
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    Debug.Log("Trigger stay");
    //    var collosionLayer = LayerMask.GetMask(LayerMask.LayerToName(other.gameObject.layer));
    //    if (collosionLayer == groundMask)
    //    {
    //        underfoot = true;
    //        if (calculateSpeed.y < 0) calculateSpeed = new Vector3(calculateSpeed.x, 0, calculateSpeed.z);
    //        currentAccel = new Vector3(currentAccel.x, 0, currentAccel.z);
    //    }
    //}

    private void OnGUI()
    {
        if (showInfo)
        {
            GUI.Label(new Rect(25, 25, Screen.width - 50, 25), "MaxSpeedX = " + maxSpeedX.ToString());
            GUI.Label(new Rect(25, 50, Screen.width - 50, 25), "Rigidbody Speed = " + rigidbodyPlayer.velocity.ToString());
            //GUI.Label(new Rect(25, 50, Screen.width - 50, 25), "Calculate Speed = " + calculateSpeed.ToString());
            GUI.Label(new Rect(25, 75, Screen.width - 50, 25), "Accelerate = " + currentAccel.ToString());
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
        if (underfoot)
        {
            //Debug.Log("Tap");
            rigidbodyPlayer.velocity = new Vector3(rigidbodyPlayer.velocity.x + jumpForce.x,
                                                   rigidbodyPlayer.velocity.y + jumpForce.y,
                                                   rigidbodyPlayer.velocity.z + jumpForce.z);

            //calculateSpeed = new Vector3(calculateSpeed.x + jumpForce.x,
            //                             calculateSpeed.y/* + jumpForce.y*/,
            //                             calculateSpeed.z + jumpForce.z);
        }
    }

    public Vector3 getCurrentSpeed()
    {
        //return calculateSpeed;
        return rigidbodyPlayer.velocity;
    }
}
