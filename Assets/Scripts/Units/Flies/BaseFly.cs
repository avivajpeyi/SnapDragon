using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseFly : MonoBehaviour
{
    public float flyValue;
    public FlyType flyType;

    public bool canMove = true;
    
    AudioClip flyDeathSound;
    GameObject flyDeathParticles;
    
    
    private void Awake() => GameManager.OnBeforeStateChanged += OnStateChanged;

    private void OnDestroy() => GameManager.OnBeforeStateChanged -= OnStateChanged;

    void Update() {
        float x1 = transform.position.x;
        if (canMove) Move();
        if (x1 > transform.position.x) {
            this.gameObject.GetComponentInChildren<SpriteRenderer>(false).flipX = false;
        } else if (transform.position.x > x1) {
            this.gameObject.GetComponentInChildren<SpriteRenderer>(false).flipX = true;
        }
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
                
                if (flyType == FlyType.Winning)
                {
                    GameManager.Instance.ChangeState(GameState.GameOver);
                }
                FlyFactory.Instance.destroyFly(this.gameObject);
                // Destroy(this.gameObject);
            }

            
        }
    }

    // protected void OnCollisionEnter2D(Collision2D other) {
    //     Debug.Log(ToString() + "has hit something");
    //     if (other.gameObject.CompareTag("GameBoundary")){
    //         Debug.Log(ToString() + "has hit the game boundary");
    //     }
    // }

    public override string ToString() {
        switch (flyType) {
            case FlyType.Line: 
                return "Line Fly";
            case FlyType.Circle: 
                return "Circle Fly";
            case FlyType.Random: 
                return "Random Fly";
            default:
                return "Invalid Dly";
        }
    }

    public abstract void Move();

    public enum FlyType {
        Line,
        Circle,
        Random,
        Winning
    }
}
