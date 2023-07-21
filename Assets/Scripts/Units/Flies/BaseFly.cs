using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseFly : MonoBehaviour
{
    public float sceneWidth;
    public Sprite flySprite;
    public float flyValue;
    public FlyType flyType;

    public BaseFly() { 
    }

    void Start() {
        BaseFlyInit();
    }

    protected void BaseFlyInit() {
        sceneWidth = Camera.main.orthographicSize * 2f * Camera.main.aspect;
    }

    void Update() {
        Move();
    }

    void onDestroy() {
        Debug.Log("Fly died");
    }

    public abstract void Move();

    public enum FlyType {
        Line,
        Circle,
        Random,
        Winning
    }
}
