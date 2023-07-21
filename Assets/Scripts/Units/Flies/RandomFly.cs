using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFly : BaseFly
{

    public RandomFly(){
        flyType = BaseFly.FlyType.Random;
    }

    void Start() {
        base.BaseFlyInit();
        RandomFlyInit();
    }

    public float moveSpeed = 2f;
    public bool isClockwise;
    public float eccentricity;

    private void RandomFlyInit() {
        // Movement logic init
        flyValue = 3;
    }

    public override void Move() {
        // Movement logic
    }
}
