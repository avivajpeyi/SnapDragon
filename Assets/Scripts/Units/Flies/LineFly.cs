using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFly : BaseFly
{
    public float moveSpeed = 2f;
    public float movementAngle;

    private Vector2 direction;

    

    public override void SetInitialReferences()
    {
        movementAngle = Random.value * 2f * Mathf.PI;
        flyValue = 1;
        direction = new Vector2(Mathf.Sin(movementAngle), Mathf.Cos(movementAngle));
        type = BaseFly.FlyType.Line;
    }


    public override void Move()
    {
        // if vel is zero, apply force in direction
        if (rb.velocity.magnitude < 0.1f)
        {
            rb.AddForce(direction * moveSpeed, ForceMode2D.Impulse);
        }
    }
}