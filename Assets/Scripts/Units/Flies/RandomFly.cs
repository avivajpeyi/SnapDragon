using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFly : BaseFly
{

    public RandomFly(){
        flyType = BaseFly.FlyType.Random;
    }

    void Start() {
        RandomFlyInit();
    }

    public float moveMin = 0.5f;
    public float moveMax = 10f;
    public float moveSpeed;
    public float directChangeThresh;
    public float movementAngle;

    private void RandomFlyInit() {
        updateMovement();
        flyValue = 3f;
        directChangeThresh = 0.97f;
    }

    public override void Move() {
        if (Random.value >= directChangeThresh) {
            updateMovement();
        }
       
        transform.position += new Vector3(Mathf.Sin(movementAngle), Mathf.Cos(movementAngle), 0) * moveSpeed * Time.deltaTime;
    }

    private void updateMovement() {
        movementAngle = Random.value * 2f * Mathf.PI;
        moveSpeed = Random.value * (moveMax - moveMin) + moveMin;
    }
}
