using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class BaseFly : MonoBehaviour
{
    public float flyValue;
    public FlyType type;

    public bool canMove = false;

    private FlyFactory myFactory;
    protected AudioClip soundFX;
    protected GameObject deathParticles;

    protected Rigidbody2D rb;
    protected SpriteRenderer sr;

    private float oldX;
    private float currentX;


    private void Awake() => GameManager.OnBeforeStateChanged += OnStateChanged;

    private void OnDestroy() => GameManager.OnBeforeStateChanged -= OnStateChanged;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        myFactory = GetComponentInParent<FlyFactory>();
        soundFX = Resources.Load<AudioClip>("FlyEatenAudio");
        deathParticles = Resources.Load<GameObject>("FlyEatenFx");
        SetInitialReferences();
    }

    public abstract void SetInitialReferences();


    [SerializeField]
    protected bool hasFlipped
    {
        get
        {
            if (type == FlyType.Winning || type == FlyType.Circle)
            {
                // These dont use physics so this idea works
                return currentX > oldX;
            }
            else
            {
                // These use physics so we need to check velocity
                return rb.velocity.x > 0;
            }
        }
    }


    void Update()
    {
        currentX = transform.position.x;
        if (canMove) Move();
        sr.flipX = hasFlipped;
        oldX = currentX;

    }


    void OnStateChanged(GameState newState)
    {
        if (newState == GameState.InGame)
        {
            canMove = true;
        }
    }


    public void OnEaten()
    {
        AudioSource.PlayClipAtPoint(soundFX, transform.position);
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        if (myFactory != null)
            myFactory.destroyFly(this.gameObject);
    }

    public override string ToString()
    {
        switch (type)
        {
            case FlyType.Line:
                return "Line Fly";
            case FlyType.Circle:
                return "Circle Fly";
            case FlyType.Random:
                return "Random Fly";
            case FlyType.Winning:
                return "Winning Fly";
            default:
                return "Invalid Dly";
        }
    }

    public abstract void Move();

    public enum FlyType
    {
        Line,
        Circle,
        Random,
        Winning
    }
}