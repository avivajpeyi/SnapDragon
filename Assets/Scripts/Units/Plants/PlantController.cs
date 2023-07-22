using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

// make an enum for Player1 and Player2
public enum PlayerNum
{
    Player1,
    Player2
}

// Class to store player keys for each player
// Jump, Left, Right
public class PlayerKeys
{
    public KeyCode jumpKey;
    public KeyCode leftKey;
    public KeyCode rightKey;

    public PlayerKeys(KeyCode jump, KeyCode left, KeyCode right)
    {
        jumpKey = jump;
        leftKey = left;
        rightKey = right;
    }
}


public class PlantController : MonoBehaviour
{
    public PlayerNum playerNum;

    [SerializeField] private int _countFliesEaten = 0;

    PlayerKeys player1Keys =
        new PlayerKeys(KeyCode.UpArrow, KeyCode.RightArrow, KeyCode.LeftArrow);

    PlayerKeys player2Keys = new PlayerKeys(KeyCode.W, KeyCode.D, KeyCode.A);


    PlayerKeys myKeys;

    // [SerializeField]
    private float RotateSpeed = 100f;

    // [SerializeField]
    private float growthSpeedMax = 10f;

    // [SerializeField]
    private float growthSpeedMin = 0.05f;

    // [SerializeField]
    private float growthAcceleration = 5f;


    private float _curSpeed;
    private float _curDist;

    [SerializeField] private float _minDist = 0.5f;
    public float maxDist = 1.0f;

    float maxDistIncrease = 1f;
    public bool canMove = false;
    LineRenderer neck;
    public Transform plantHead;

    bool growKeyDown = false;
    bool leftKeyDown = false;
    bool rightKeyDown = false;


    private void Awake() => GameManager.OnBeforeStateChanged += OnStateChanged;

    private void OnDestroy() => GameManager.OnBeforeStateChanged -= OnStateChanged;


    void OnStateChanged(GameState newState)
    {
        if (newState == GameState.Starting)
        {
            ResetStats();
        }

        else if (newState == GameState.InGame)
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        neck = GetComponent<LineRenderer>();
        neck.SetPosition(0, this.transform.position);
        neck.SetPosition(1, plantHead.position);
        if (playerNum == PlayerNum.Player1)
        {
            myKeys = player1Keys;
        }
        else
        {
            myKeys = player2Keys;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove) HandleInput();
    }

    public void Move(InputAction.CallbackContext context)
    {
        // Grow while up pressed
        if (context.ReadValue<Vector2>().y > 0)
        {
            growKeyDown = true;
            Debug.Log("grow");
        }
        else
        {
            growKeyDown = false;
        }

        // rotate
        if (context.ReadValue<Vector2>().x > 0)
        {
            leftKeyDown = true;
            Debug.Log("left");
        }
        else if (context.ReadValue<Vector2>().x < 0)
        {
            rightKeyDown = true;
            Debug.Log("right");
        }
        else
        {
            leftKeyDown = false;
            rightKeyDown = false;
        }
    }

    void HandleInput()
    {
        // if the player presses the space bar, grow the plant
        if (Input.GetKey(myKeys.jumpKey)) Grow();
        else
        {
            // TODO: prevent going below screen
            if (Input.GetKey(myKeys.leftKey))
                transform.Rotate(Vector3.forward * -RotateSpeed * Time.deltaTime);
            else if (Input.GetKey(myKeys.rightKey))
                transform.Rotate(Vector3.forward * RotateSpeed * Time.deltaTime);

            ResetPosition();
        }

        neck.SetPosition(0, this.transform.position);
        neck.SetPosition(1, plantHead.position);
    }


    bool HeadHasNotReachedMaxDist
    {
        get
        {
            _curDist = Vector3.Distance(plantHead.position, this.transform.position);
            return _curDist < maxDist;
        }
    }

    public void Grow()
    {
        if (HeadHasNotReachedMaxDist)
        {
            float newSpeed = _curSpeed - growthAcceleration * Time.deltaTime;
            _curSpeed = Mathf.Max(newSpeed, growthSpeedMin);
            plantHead.localPosition += new Vector3(0, _curSpeed * Time.deltaTime, 0);
        }
    }

    public void ResetPosition()
    {
        // back to original y (keep x and z)
        plantHead.localPosition = new Vector3(plantHead.localPosition.x, _minDist,
            plantHead.localPosition.z);
        neck.SetPosition(0, this.transform.position);
        neck.SetPosition(1, plantHead.position);
        _curSpeed = growthSpeedMax;
    }

    public void OnFlyEaten()
    {
        _countFliesEaten++;
        ResetPosition();
        maxDist += maxDistIncrease;
        growthSpeedMax += growthAcceleration*.5f;
    }


    public void ResetStats()
    {
        _countFliesEaten = 0;
        maxDist = _minDist;
        ResetPosition();
    }
}