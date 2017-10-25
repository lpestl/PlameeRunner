using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour {
#region Shame and disgrace.Do not look, plz!
    public LayerMask groundMask;
    public LayerMask playerMask;

    private Rigidbody rigidbodyObstacle;
    public Vector3 gravityAccel = new Vector3(0.0f, -9.8f, 0.0f);
    private bool onGround = false;

    void Start ()
    {
        Physics.gravity = gravityAccel;
        rigidbodyObstacle = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
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
#endregion
}
