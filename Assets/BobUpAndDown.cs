using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BobUpAndDown : MonoBehaviour
{
    float bobHeight;

    private void Start()
    {
        bobHeight = 1f;
        // Do Tween bob up and down, pulsate 
        transform.DOLocalMoveY(transform.position.y - bobHeight, 1f).SetEase(Ease.InOutSine).SetLoops(
            -1, LoopType.Yoyo
        );
    }
}