using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseFly : MonoBehaviour
{
    public Sprite flySprite;
    public float flyValue;

    public AudioClip flyDeathSound;
    public GameObject flyDeathParticles;
    
    void Update() {
        Move();
    }

    void OnDestroy() {
        // Debug.Log("Fly died");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.CompareTag("Plant"))
        {
            Debug.Log("Fly collided with " + col.gameObject.name);
            PlantController plant = col.GetComponentInParent<PlantController>();
            if (plant != null)
            {
                plant.OnFlyEaten();
                Destroy(this.gameObject);
                // Play death sound and instantiate particles
                flyDeathSound = Resources.Load<AudioClip>("FlyEatenAudio");
                flyDeathParticles = Resources.Load<GameObject>("FlyEatenFx");
                AudioSource.PlayClipAtPoint(flyDeathSound, transform.position);
                Instantiate(flyDeathParticles, transform.position, Quaternion.identity);
            }

            
        }
    }

    public abstract void Move();
}
