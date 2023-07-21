using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseFly : MonoBehaviour
{
    public Sprite flySprite;
    public float flyValue;

    void Update() {
        Move();
    }

    void onDestroy() {
        Debug.Log("Fly died");
    }

    public abstract void Move();
}
