using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantheadCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("DragonHead (SPRITE) collides with " + col.gameObject.name);
        if (col.gameObject.CompareTag("Plant"))
            Debug.Log("YAYY dragon-dragon collision. Please KISS!");
    }
}
