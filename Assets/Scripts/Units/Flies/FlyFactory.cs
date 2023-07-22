using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyFactory : MonoBehaviour
{

    public float sceneWidth;
    public float sceneHeight;
    public GameObject LineFly;
    public GameObject CircleFly;
    public GameObject RandomFly;

    public int framesPerSpawn;
    private int frameCount;

    public float lineFlySpawnFrequency;
    public float lineFlySpawnMean;
    public int lineFlySpawnMax;
    public float circleFlySpawnFrequency;
    public float circleFlySpawnMean;
    public int circleFlySpawnMax;
    public float randomFlySpawnFrequency;
    public float randomFlySpawnMean;
    public int randomFlySpawnMax;
    public List<LineFly> lineFlyList = new List<LineFly>();
    public List<CircleFly> circleFlyList = new List<CircleFly>();
    public List<RandomFly> randomFlyList = new List<RandomFly>();

    private static FlyFactory instance;

    public static FlyFactory Instance {
        get {
            if (instance == null)
            {
                instance = new GameObject("FlySpawner").AddComponent<FlyFactory>();
            }
            return instance;
        }
    }



    void Start() {
    CalculateScreneSize();
    framesPerSpawn = 540;
    frameCount = 0;
    lineFlySpawnFrequency = 1;
    lineFlySpawnMean = 6;
    lineFlySpawnMax = 10;
    circleFlySpawnFrequency = 2;
    circleFlySpawnMean = 3;
    circleFlySpawnMax = 6;
    randomFlySpawnFrequency = 3;
    randomFlySpawnMean = 2;
    randomFlySpawnMax = 4; 
    }

    void Update() {
        frameCount++;
        if (frameCount != framesPerSpawn) {
            return;
        } else {
            Debug.Log("Spawn Tick");
            frameCount = 0;
        }
        if (Random.value < (1 - lineFlyList.Count/lineFlySpawnMax)) {
            lineFlyList.Add((LineFly) CreateFly(BaseFly.FlyType.Line));
        }
        if (Random.value < (1 - circleFlyList.Count/circleFlySpawnMax)) {
            circleFlyList.Add((CircleFly) CreateFly(BaseFly.FlyType.Circle));
        }
        if (Random.value < (1 - randomFlyList.Count/randomFlySpawnMax)) {
            randomFlyList.Add((RandomFly) CreateFly(BaseFly.FlyType.Random));
        }
    }

    public BaseFly CreateFly(BaseFly.FlyType type) {
        BaseFly fly = null;
        GameObject prefabType;

        switch (type)
        {
            case BaseFly.FlyType.Line:
                fly = new GameObject("LineFly").AddComponent<LineFly>();
                prefabType = LineFly;
                break;
            case BaseFly.FlyType.Circle:
                fly = new GameObject("CircleFly").AddComponent<CircleFly>();;
                prefabType = CircleFly;
                break;
            case BaseFly.FlyType.Random:
                fly = new GameObject("RandomFly").AddComponent<RandomFly>();;
                prefabType = RandomFly;
                break;
            default:
                Debug.LogError("Invalid Fly Type: " + type);
                prefabType = null;
                fly = new LineFly();
                return fly;
        }

        Vector3 summonPosition = generatePosition();
        Quaternion summonRotation = transform.rotation;

        GameObject summonedPrefab = Instantiate(prefabType, summonPosition, summonRotation);


        return fly;
    }

    private void CalculateScreneSize() 
    {
        float halfHeight = Camera.main.orthographicSize;
        float halfWidth = halfHeight * Camera.main.aspect;

        sceneWidth = halfWidth * 2f;
        sceneHeight = halfHeight * 2f;
    }

    private Vector3 generatePosition() {
        float xPos = Random.value * (sceneWidth) - sceneWidth/2;
        float yPos = Random.value * (sceneHeight) - sceneHeight/2;

        return new Vector3(xPos,yPos,0);
    }

    public void destroyFly(BaseFly fly, BaseFly.FlyType type) {
        switch (type){
            case BaseFly.FlyType.Line:
                LineFly lFly = (LineFly) fly;
                if (lineFlyList.Contains(lFly)) {
                    lineFlyList.Remove(lFly);
                } else {
                    Debug.LogError("Error deleting line fly, does not exist?");
                }
                break;
            case BaseFly.FlyType.Circle:
                CircleFly cFly = (CircleFly) fly;
                if (circleFlyList.Contains(cFly)) {
                    circleFlyList.Remove(cFly);
                } else {
                    Debug.LogError("Error deleting circle fly, does not exist?");
                }
                break;
            case BaseFly.FlyType.Random:
                RandomFly rFly = (RandomFly) fly;
                Debug.Log(rFly);
                Debug.Log(randomFlyList);
                if (randomFlyList.Contains(rFly)) {
                    randomFlyList.Remove(rFly);
                } else {
                    Debug.LogError("Error deleting random fly, does not exist?");
                }
                break;
            default:
                 Debug.LogError("Error deleting fly. Invalid type" + type);
                 break;
        }
    }

}
