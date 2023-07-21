using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseFly : MonoBehaviour
{
    public Sprite flySprite;
    public float flyValue;
    public FlyFactory.FlyType flyType;

    public bool canMove = true;
    
    AudioClip flyDeathSound;
    GameObject flyDeathParticles;
    
    
    private void Awake() => GameManager.OnBeforeStateChanged += OnStateChanged;

     private void OnDestroy() => GameManager.OnBeforeStateChanged -= OnStateChanged;

    
    void Update() {
        if (canMove) Move();
    }

    
    void OnStateChanged(GameState newState) {
        if (newState == GameState.InGame) {
            canMove = true;
        }
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
                // Play death sound and instantiate particles
                flyDeathSound = Resources.Load<AudioClip>("FlyEatenAudio");
                flyDeathParticles = Resources.Load<GameObject>("FlyEatenFx");
                
                // The following will be needed in the scene when we have music
                // AudioSystem.Instance.PlaySound(flyDeathSound);
                // for now will just use a 'PlayOneShot' method
                AudioSource.PlayClipAtPoint(flyDeathSound, transform.position);
                
                Instantiate(flyDeathParticles, transform.position, Quaternion.identity);
                
                // HERE WE CAN ALSO ADD LOGIC OF FLY-VALUE MULTIPLIER
                
                if (flyType == FlyFactory.FlyType.Winning)
                {
                    GameManager.Instance.ChangeState(GameState.GameOver);
                }

                
                
                Destroy(this.gameObject);
            }

            
        }
    }

    public abstract void Move();
}
