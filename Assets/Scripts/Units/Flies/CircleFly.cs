using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CircleFly : BaseFly
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
        type = BaseFly.FlyType.Circle;
        startingPosition = transform.position;
        timeElapsed = Random.value * 2f * Mathf.PI;
        if (Random.value > 0.5)
        {
            direction = 1f;
        }
        else
        {
            direction = -1f;
        }

        eccentricity = Random.Range(0.0f, 0.7f);
        semiMinorAxis = 1f;
        semiMajorAxis = semiMinorAxis / Mathf.Sqrt(1f - eccentricity * eccentricity);
        flyValue = 2;
    }

    public override void Move()
    {
        timeElapsed += Time.deltaTime;
        
        
        float newX = semiMajorAxis * eccentricity +
                     semiMajorAxis * Mathf.Cos(direction * moveSpeed * timeElapsed) +
                     startingPosition.x;
        float newY = semiMinorAxis * Mathf.Sin(direction * moveSpeed * timeElapsed) +
                     startingPosition.y;

        transform.position = new Vector3(newX, newY, 0);
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        // Rotate in the other direction
        // direction *= -1f;
    }
}