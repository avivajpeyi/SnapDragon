using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleFly : BaseFly
{

    public CircleFly(){
        flyType = BaseFly.FlyType.Circle;
    }

    void Start() {
        base.BaseFlyInit();
        CircleFlyInit();
    }

    public float moveSpeed = 2f;
    public bool isClockwise;
    public float eccentricity;

    private void CircleFlyInit() {
        // Movement logic init
        flyValue = 2;
    }

    public override void Move() {
        // Movement logic
    }
}
