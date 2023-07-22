using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFly : BaseFly
{
    float moveForceMin = 0.5f;
    float moveForceMax = 10f;
    public float moveForce;
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
        movementAngle = Random.value * 2f * Mathf.PI;
        moveForce = Random.value * (moveForceMax - moveForceMin) + moveForceMin;
        Vector2 dir = new Vector2(Mathf.Sin(movementAngle), Mathf.Cos(movementAngle));

        rb.AddForce(dir * moveForce, ForceMode2D.Impulse);
    }


    // private void RandomFlyInit() {
    //     updateMovement();
    //     flyValue = 3f;
    //     directChangeThresh = 0.97f;
    // }
    //
    // public override void Move() {
    //     if (Random.value >= directChangeThresh) {
    //         updateMovement();
    //     }
    //    
    //     transform.position += new Vector3(Mathf.Sin(movementAngle), Mathf.Cos(movementAngle), 0) * moveSpeed * Time.deltaTime;
    // }
    //
    // private void updateMovement() {
    //     movementAngle = Random.value * 2f * Mathf.PI;
    //     moveSpeed = Random.value * (moveMax - moveMin) + moveMin;
    // }
}