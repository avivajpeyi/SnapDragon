using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] private CinemachineVirtualCamera vcam_follow;
    [SerializeField] private CinemachineVirtualCamera vcam_goal;


    private void Awake() => GameManager.OnBeforeStateChanged += OnStateChanged;

    private void OnDestroy() => GameManager.OnBeforeStateChanged -= OnStateChanged;


    void OnStateChanged(GameState state)
    {
        if (state==GameState.Starting)
        {
            PrioritizeGoal();
        }
        else if (state == GameState.InGame)
        {
            PrioritizeFollow();
        }
        else if (state == GameState.GameOver)
        {
            PrioritizeGoal();
        }
    }


    public void PrioritizeGoal()
    {
        vcam_goal.Priority = 10;
        vcam_follow.Priority = 0;
        Debug.Log("Camera switch to PrioritizeGoal");
    }

    public void PrioritizeFollow()
    {
        vcam_goal.Priority = 0;
        vcam_follow.Priority = 10;
        Debug.Log("Camera switch to PrioritizeFollow");
    }


    private void Start()
    {
        if (vcam_goal == null)
        {
            Debug.LogError("vcam_goal is null");
        }
        if (vcam_follow == null)
        {
            Debug.LogError("vcam_follow is null");
        }
        PrioritizeGoal();
    }
}