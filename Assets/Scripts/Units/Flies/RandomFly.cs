using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFly : BaseFly
{
    float moveMin = 0.5f;
    float moveMax = 10f;
    public float moveSpeed;
    float directChangeThresh;
    float movementAngle;

    public RandomFly()
    {
        type = BaseFly.FlyType.Random;
    }

    public override void SetInitialReferences()
    {
        flyValue = 3f;
        // Random flopat between 0.95 and 1
        directChangeThresh = Random.Range(0.95f, 1f);
    }


    public override void Move()
    {
        if (Random.value >= directChangeThresh)
        {
            GetKicked();
        }
    }

    void GetKicked()
    {
        // zero current velocity
        rb.velocity = Vector2.zero;
        // Add small force in new direction
        rb.AddForce(
            new Vector2(Mathf.Sin(movementAngle), Mathf.Cos(movementAngle)) * (moveSpeed * 0.1f),
            ForceMode2D.Impulse);
    }

}