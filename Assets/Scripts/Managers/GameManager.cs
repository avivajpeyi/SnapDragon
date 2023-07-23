using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : StaticInstance<GameManager>
{
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    public GameState State { get; private set; }

    // Kick the game off with the first state
    // void Start() => ChangeState(GameState.Starting);

    // FIXME: Currently, the game starts in the InGame state -- this is just for testing purposes
    void Start()
    {
        ChangeState(GameState.Starting);
    }

    private void Update()
    {
        // if any key down
        if (Input.anyKeyDown && State == GameState.Starting)
        {
            ChangeState(GameState.InGame);
        }

        // If press R
        if (Input.anyKeyDown && State == GameState.GameOver)
        {
            // Restart the game
            // ChangeState(GameState.Starting);
            // FUck it, just reload the bloody scene
            // reload current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void ChangeState(GameState newState)
    {
        Debug.Log("<color=cyan>CHANGE STATE: </color> " + State + "-->" + newState);

        OnBeforeStateChanged?.Invoke(newState);


        State = newState;
        switch (newState)
        {
            case GameState.Starting:
                HandleStarting();
                break;
            case GameState.InGame:
                HandleInGame();
                break;
            case GameState.GameOver:
                HandleGameOver();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnAfterStateChanged?.Invoke(newState);

        Debug.Log($"New state: {newState}");
    }

    private void HandleStarting()
    {
        // Do some start setup (spawn flys, etc)

        // Eventually call ChangeState again with your next state

        // ChangeState(GameState.InGame);
    }

    private void HandleInGame()
    {
        // Enable player controls, allow flys to move
    }

    private void HandleGameOver()
    {
        // Pause everythinig's movement
        
        
        
        
        
    }
}


[Serializable]
public enum GameState
{
    Starting = 0,
    InGame = 2,
    GameOver = 5,
}