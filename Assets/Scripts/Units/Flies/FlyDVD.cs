using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
Adds DVD-type Movement to Fly

Todo:
[] Successful Fly Attempt


*/
public class FlyDVD : BaseFly
{

    public float moveSpeed = 2f;
    public float speed = 5.0f;
    public float minX = -10.0f;
    public float maxX = 10.0f;
    public float minY = -5.0f;
    public float maxY = 5.0f;
    public Vector3 direction = new Vector3(1, 1);


    // public FlyDVD() {
    //     // Sprite = ____
    //     flyValue = 1;
    //     Debug.Log("Helloworld!   DVDFly's FlyValue changed to 1");
    //     // direction = new Vector3(1, 1);
    //     direction = direction.normalized;
    // }

    public override void SetInitialReferences()
    {
        // pass
    }

    public override void Move() {
        // transform.position += new Vector3(0,1,0) * moveSpeed * Time.deltaTime;
        //move dvd logo in current direction and speed
        transform.position += direction * moveSpeed * Time.deltaTime;
        //check DVD logo reaches screen edges and update direction accordingly
        if(transform.position.x < minX || transform.position.x > maxX)
            direction.x *= -1;
        if(transform.position.y < minY || transform.position.y > maxY)
            direction.y *= -1;
    }
}