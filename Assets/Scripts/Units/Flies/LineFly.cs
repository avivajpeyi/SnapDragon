using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFly : BaseFly
{

    public LineFly(){
        flyType = BaseFly.FlyType.Line;
    }

    void Start() {
        LineFlyInit();
    }

    public float moveSpeed = 2f;
    public float movementAngle;

    private void LineFlyInit() {
        movementAngle = Random.value * 2f * Mathf.PI;
        flyValue = 1;
    }

    public override void Move() {
        transform.position += new Vector3(Mathf.Sin(movementAngle), Mathf.Cos(movementAngle), 0) * moveSpeed * Time.deltaTime;
    }
}
