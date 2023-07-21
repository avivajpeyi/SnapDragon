using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFly : BaseFly
{

    public float moveSpeed = 2f;

    public LineFly() {
        // Sprite = ____
        flyValue = 1;
    }

    public override void Move() {
        transform.position += new Vector3(1,0,0) * moveSpeed * Time.deltaTime;
    }
}
