using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyFactory : MonoBehaviour
{
    public FlyFactory() {
        // Globals
    }

    void Update() {
        FlyBehaviour flyBehaviour = GetComponent<FlyBehaviour>();
        flyBehaviour.fly = CreateFly(FlyType.Line);
    }

    public BaseFly CreateFly(FlyType type) {
        BaseFly fly = null;

        switch (type)
        {
            case FlyType.Line:
                fly = new LineFly();
                break;
            default:
                Debug.LogError("Invalid Fly Type: " + type);
                break;
        }

        return fly;
    }

    public enum FlyType
    {
        Line,
        Circle,
        Random,
        Winning
    }
}
