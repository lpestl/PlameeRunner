using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    #region Properties
    public bool showInfo = true;
    public float maxSpeedX = 50.0f;
    
    public Vector3 acceleration = new Vector3(10.0f, 0.0f, 0.0f);
    public Vector3 gravityAccel = new Vector3(0.0f, -9.8f, 0.0f);
    protected Vector3 currentAccel;
    public Vector3 jumpForce = new Vector3(0.0f, 10.0f, 0.0f);

    public LayerMask groundMask;
    public LayerMask obstacleMask;

    public float dieY = -15.0f;

    public bool underfoot = false;
    private Rigidbody rigidbodyPlayer;
    #endregion

    // TODO lpestl: In the dead code there are remnants of physics without rigidbodi. It will be necessary to bring to mind.
    #region Start and Update
    void Start () {
        rigidbodyPlayer = GetComponent<Rigidbody>();
        currentAccel = acceleration + gravityAccel;
        ChangeColor(Color.white);
    }
	
	void Update () {
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

        CalculateAcceleration();

        if (transform.position.y < dieY)
        {
            EchoLog.Print("[OPS] I fell into a hole.");
            LevelEventSystem.GameOver();
        }
    }
    #endregion

    #region virtual methods for the heir.
    public virtual void CalculateAcceleration()
    {
        if (rigidbodyPlayer.velocity.x >= maxSpeedX)
        {
            currentAccel = gravityAccel;
        }
        else
        {
            currentAccel = acceleration + gravityAccel;
        }
    }

    public virtual void ChangeColor(Color color)
    {
        var rend = transform.GetComponent<MeshRenderer>();
        if (rend.material.color != color)
        {
            rend.material.color = color;
        }
    }
    #endregion

    #region Check collisions
    void OnCollisionEnter(Collision collision)
    {
        var collosionLayer = LayerMask.GetMask(LayerMask.LayerToName(collision.collider.gameObject.layer));
        if (collosionLayer == groundMask)
        {
            underfoot = true;
            ChangeColor(Color.green);
        }

        if (collosionLayer == obstacleMask)
        {
            EchoLog.Print("[OPS] Touch obstacle " + collision.gameObject.name);
            ChangeColor(Color.red);
            LevelEventSystem.GameOver();
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {
        var collosionLayer = LayerMask.GetMask(LayerMask.LayerToName(collision.collider.gameObject.layer));
        if (collosionLayer == groundMask)
        {
            underfoot = false;
            ChangeColor(Color.yellow);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            underfoot = true;
            ChangeColor(Color.green);
        }
    }
    #endregion

#region Show Info using old gui
    private void OnGUI()
    {
        if (showInfo)
        {
            GUI.Label(new Rect(25, 25, Screen.width - 50, 25), "MaxSpeedX = " + maxSpeedX.ToString());
            GUI.Label(new Rect(25, 50, Screen.width - 50, 25), "Speed = " + rigidbodyPlayer.velocity.ToString());
            GUI.Label(new Rect(25, 75, Screen.width - 50, 25), "Accelerate = " + currentAccel.ToString());
        }
    }

    public Vector3 getCurrentSpeed()
    {
        return rigidbodyPlayer.velocity;
    }
#endregion

    #region Subscrube on tap
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
            rigidbodyPlayer.velocity = new Vector3(rigidbodyPlayer.velocity.x + jumpForce.x,
                                                   rigidbodyPlayer.velocity.y + jumpForce.y,
                                                   rigidbodyPlayer.velocity.z + jumpForce.z);
        }
    }
    #endregion
}
