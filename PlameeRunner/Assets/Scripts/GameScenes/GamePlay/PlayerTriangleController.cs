using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriangleController : PlayerController {

    public override void ChangeColor(Color color)
    {
        var rend = transform.GetComponent<SpriteRenderer>();
        if (rend.color != color)
        {
            rend.color = color;
        }
    }

    public override void CalculateAcceleration()
    {
        base.CalculateAcceleration();

        var rigBody = GetComponent<Rigidbody>();
        if ((rigBody.velocity.x > (maxSpeedX / 2)) && (rigBody.velocity.x < maxSpeedX))
        {
            base.currentAccel = new Vector3(currentAccel.x * currentAccel.x, currentAccel.y, currentAccel.z);
        }
    }
}
