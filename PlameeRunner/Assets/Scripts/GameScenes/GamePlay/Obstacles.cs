using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour {

    public LayerMask groundMask;
    public LayerMask playerMask;

    private Rigidbody rigidbodyObstacle;
    public Vector3 gravityAccel = new Vector3(0.0f, -9.8f, 0.0f);
    private bool onGround = false;
    // Use this for initialization
    void Start ()
    {
        Physics.gravity = gravityAccel;
        rigidbodyObstacle = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (!onGround)
    //    {
    //        rigidbodyPlayer.velocity = new Vector3(rigidbodyPlayer.velocity.x + gravityAccel.x * Time.deltaTime,
    //                                               rigidbodyPlayer.velocity.y + gravityAccel.y * Time.deltaTime,
    //                                               rigidbodyPlayer.velocity.z + gravityAccel.z * Time.deltaTime);
    //    }
    //}

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision Enter");
        var collosionLayer = LayerMask.GetMask(LayerMask.LayerToName(collision.collider.gameObject.layer));
        if (collosionLayer == groundMask)
        {
            onGround = true;

        }

        if (collosionLayer == playerMask)
        {
            rigidbodyObstacle.constraints = RigidbodyConstraints.FreezePositionZ |
                                            RigidbodyConstraints.FreezeRotationX |
                                            RigidbodyConstraints.FreezeRotationY;
        }
    }

    //private void OnCollisionExit(Collision collision)
    //{
    //    //Debug.Log("Collision Exit");
    //    var collosionLayer = LayerMask.GetMask(LayerMask.LayerToName(collision.collider.gameObject.layer));
    //    if (collosionLayer == groundMask)
    //    {
    //        onGround = false;
    //    }
    //}
}
