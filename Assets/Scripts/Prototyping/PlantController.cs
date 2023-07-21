using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    
    [SerializeField]
    private int CountFliesEaten = 0;

    PlayerKeys player1Keys =
        new PlayerKeys(KeyCode.UpArrow, KeyCode.RightArrow, KeyCode.LeftArrow);

    PlayerKeys player2Keys = new PlayerKeys(KeyCode.W, KeyCode.D, KeyCode.A);
    PlayerKeys myKeys;

    public float RotateSpeed = 100f;
    public float growthRate = 0.1f;
    public float maxDist = 1.0f;
    public float maxDistIncrease = 1f;

    LineRenderer neck;
    public Transform plantHead;

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
        HandleInput();
    }

    void HandleInput()
    {
        // if the player presses the space bar, grow the plant
        if (Input.GetKey(myKeys.jumpKey))
        {
            Grow();
        }
        else
        {
            ResetPosition();
        }


        // if the player presses the left or right arrow keys, rotate the plant around the z axis
        if (Input.GetKey(myKeys.leftKey))
            transform.Rotate(Vector3.forward * -RotateSpeed * Time.deltaTime);
        else if (Input.GetKey(myKeys.rightKey))
            transform.Rotate(Vector3.forward * RotateSpeed * Time.deltaTime);

        neck.SetPosition(0, this.transform.position);
        neck.SetPosition(1, plantHead.position);
    }


    public void Grow()
    {
        if (plantHead.localPosition.y < maxDist)
        {
            plantHead.localPosition += new Vector3(0, growthRate, 0);
        }
    }

    public void ResetPosition()
    {
        // back to original position
        plantHead.localPosition = new Vector3(0, 0, 0);
        neck.SetPosition(0, this.transform.position);
        neck.SetPosition(1, plantHead.position);
    }
    
    public void OnFlyEaten()
    {
        CountFliesEaten++;
        maxDist+=maxDistIncrease;
    }
}