using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DoTweenDisolve : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material.SetFloat("_DissolveAmount", 1);
        // DOTween shrinks the sprite and then destroys it
        transform.DOScale(0, 0.5f).SetEase(Ease.InBack);
        spriteRenderer.material.DOFloat(0, "_DissolveAmount", 0.5f).SetEase(Ease.InBack).OnComplete(() => Destroy(gameObject));
    }
}
