using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public TMP_Text mainText;


    private void Awake() => GameManager.OnBeforeStateChanged += OnStateChanged;

    private void OnDestroy() => GameManager.OnBeforeStateChanged -= OnStateChanged;


    void OnStateChanged(GameState newState)
    {
        if (newState == GameState.Starting)
        {
            mainText.text = "Press any key to start";
        }
        else if (newState == GameState.GameOver)
        {
            mainText.text = "GAME OVER";
        }
        else
        {
            mainText.text = "";
        }
    }
}