using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFly : BaseFly
{
    
    float minTimeBetweenKicks;
    float maxTimeBetweenKicks;
    
    float moveForceMin;
    float moveForceMax;
    public float moveForce;
    float movementAngle;
    bool canGetKicked = true;


    public RandomFly()
    {
        minTimeBetweenKicks = 0.5f;
        maxTimeBetweenKicks = 3f;
        moveForceMin = 0.5f;
        moveForceMax = 7f;
        type = BaseFly.FlyType.Random;
    }

    public override void SetInitialReferences()
    {
        flyValue = 3f;
    }



    
    public override void Move()
    {
        if (canGetKicked)
        {
            GetKicked();
            canGetKicked = false;
            // Call IENumearator 1-4 seconds before being able to get kicked again
            StartCoroutine(ResetCanGetKicked(Random.Range(minTimeBetweenKicks, maxTimeBetweenKicks)));
            
        }
    }
    
    IEnumerator ResetCanGetKicked(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        canGetKicked = true;
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