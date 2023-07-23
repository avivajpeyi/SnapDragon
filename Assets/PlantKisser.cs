using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantKisser : MonoBehaviour
{
    WordArtController wordArtController;
    private PlantController me;

    void Start()
    {
        wordArtController = FindObjectOfType<WordArtController>();
        me = transform.GetComponentInParent<PlantController>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Plant"))
        {
            Debug.Log("Plant collides with " + col.gameObject.name);
            PlantController other = col.gameObject.GetComponentInParent<PlantController>();
            wordArtController.spawnWordArt(transform.position,
                WordArtController.WordArtTypes.Smooch);
            other.ResetPosition();
            me.ResetPosition();
        }
    }
}