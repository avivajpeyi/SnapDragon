using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenFly : BaseFly
{

    

    public float moveSpeed = 3f;
    public float direction;
    public float eccentricity;

    private float semiMajorAxis;
    private float semiMinorAxis;
    private float timeElapsed;
    private Vector3 startingPosition;

    public override void SetInitialReferences()
    {
        type = BaseFly.FlyType.Winning;
        startingPosition = transform.position;
        timeElapsed = 0;
        if (Random.value > 0.5) {
            direction = 1f;
        } else {
            direction = -1f;
        } 
        eccentricity = 0.5f;
        semiMinorAxis = 1f;
        semiMajorAxis = semiMinorAxis / Mathf.Sqrt(1f - eccentricity * eccentricity);
        flyValue = 1000000000;
    }

    public override void Move() {
        timeElapsed += Time.deltaTime;

        float newX = semiMajorAxis * eccentricity + semiMajorAxis * Mathf.Cos(direction * moveSpeed * timeElapsed) + startingPosition.x;
        float newY = semiMinorAxis * Mathf.Sin(direction * moveSpeed * timeElapsed) + startingPosition.y;

        transform.position = new Vector3(newX, newY, 0);
    }
}
