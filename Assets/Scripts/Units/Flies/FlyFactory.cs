using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyFactory : MonoBehaviour
{
    public GameObject LineFly;
    public GameObject CircleFly;
    public GameObject RandomFly;

    public float lineFlySpawnFrequency = 100000000; //Need Units
    public float circleFlySpawnFrequency = 20000000;
    public float randomFlySpawnFrequency = 10000000;

    private int timeTrack = 0;

    void Update() {
        Debug.Log("HEY");
        
        timeTrack++;

        if (timeTrack % randomFlySpawnFrequency == 0) {
            CreateFly(BaseFly.FlyType.Line);
        }
        if (timeTrack % circleFlySpawnFrequency == 0) {
            CreateFly(BaseFly.FlyType.Circle);
        }
        if (timeTrack % lineFlySpawnFrequency == 0) {
            CreateFly(BaseFly.FlyType.Random);
        }
        
    }

    public BaseFly CreateFly(BaseFly.FlyType type) {
        BaseFly fly = null;
        GameObject prefabType;

        switch (type)
        {
            case BaseFly.FlyType.Line:
                fly = new LineFly();
                prefabType = LineFly;
                break;
            case BaseFly.FlyType.Circle:
                fly = new CircleFly();
                prefabType = CircleFly;
                break;
            case BaseFly.FlyType.Random:
                fly = new RandomFly();
                prefabType = RandomFly;
                break;
            default:
                Debug.LogError("Invalid Fly Type: " + type);
                prefabType = null;
                fly = new LineFly();
                return fly;
        }

        Vector3 summonPosition = transform.position;
        Quaternion summonRotation = transform.rotation;

        GameObject summonedPrefab = Instantiate(prefabType, summonPosition, summonRotation);


        return fly;
    }


}
