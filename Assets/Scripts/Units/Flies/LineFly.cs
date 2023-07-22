using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFly : BaseFly
{
    public float moveSpeed = 2f;
    public float movementAngle;

    private Vector2 direction;

    public LineFly()
    {
        type = BaseFly.FlyType.Line;
    }

    public override void SetInitialReferences()
    {
        movementAngle = Random.value * 2f * Mathf.PI;
        flyValue = 1;
        direction = new Vector2(Mathf.Sin(movementAngle), Mathf.Cos(movementAngle));
        // Send the fly off in this direction (apply a force, no drag)
        rb.AddForce(direction * moveSpeed, ForceMode2D.Impulse);
    }


    public override void Move()
    {
        // rb.velocity = direction * moveSpeed;
    }
}