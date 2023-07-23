using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BobUpAndDown : MonoBehaviour
{
    float bobHeight = 100f;

    private void Start()
    {
        // Do Tween bob up and down, pulsate 
        transform.DOLocalMoveY(bobHeight, 1f).SetEase(Ease.InOutSine).SetLoops(
            -1, LoopType.Yoyo
        );
    }
}