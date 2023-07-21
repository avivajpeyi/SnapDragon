using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantHead : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Fly")
        {
            Debug.Log("PlantHead collided with Fly");
            Destroy(col.gameObject);
        }
    }
}
