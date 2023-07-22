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
    public int lineFlyCount;
    public int lineFlySpawnMax;
    public float circleFlySpawnFrequency;
    public int circleFlyCount;
    public int circleFlySpawnMax;
    public float randomFlySpawnFrequency;
    public int randomFlyCount;
    public int randomFlySpawnMax;
    public List<GameObject> flyList = new List<GameObject>();

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
    lineFlySpawnMax = 10;
    circleFlySpawnFrequency = 2;
    circleFlySpawnMax = 6;
    randomFlySpawnFrequency = 3;
    randomFlySpawnMax = 4; 
    lineFlyCount = 0;
    circleFlyCount = 0;
    randomFlyCount = 0;
    }

    void Update() {
        frameCount++;
        if (frameCount != framesPerSpawn) {
            return;
        } else {
            Debug.Log("Spawn Tick");
            frameCount = 0;
        }
        if (Random.value < (1 - lineFlyCount/lineFlySpawnMax)) {
            // flyList.Add(CreateFly(BaseFly.FlyType.Line));
            CreateFly(BaseFly.FlyType.Line);
            lineFlyCount++;
        }
        if (Random.value < (1 - circleFlyCount/circleFlySpawnMax)) {
            // flyList.Add(CreateFly(BaseFly.FlyType.Circle));
            CreateFly(BaseFly.FlyType.Circle);
            circleFlyCount++;
        }
        if (Random.value < (1 - randomFlyCount/randomFlySpawnMax)) {
            // flyList.Add(CreateFly(BaseFly.FlyType.Random));
            CreateFly(BaseFly.FlyType.Random);
            randomFlyCount++;
        }
    }

    public GameObject CreateFly(BaseFly.FlyType type) {
        GameObject prefabType;

        switch (type)
        {
            case BaseFly.FlyType.Line:
                prefabType = LineFly;
                break;
            case BaseFly.FlyType.Circle:
                prefabType = CircleFly;
                break;
            case BaseFly.FlyType.Random:
                prefabType = RandomFly;
                break;
            default:
                Debug.LogError("Invalid Fly Type: " + type);
                prefabType = LineFly;
                break;
        }

        Vector3 summonPosition = generatePosition();
        Quaternion summonRotation = transform.rotation;
        Debug.Log("Prefab: " + prefabType);
        GameObject fly = Instantiate(prefabType, summonPosition, summonRotation);

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

    public void destroyFly(GameObject fly) {
        // if (flyList.Contains(fly)) {
        //     flyList.Remove(fly);
        //     Destroy(fly);
        // } else {
        //     Debug.LogError("Error deleting fly, does not exist?");
        // }

        if (fly.GetComponent<LineFly>() != null) {
            lineFlyCount -= 1;
            Debug.Log("Line Fly removed");
        }
        if (fly.GetComponent<CircleFly>() != null) {
            circleFlyCount -= 1;
            Debug.Log("Circle Fly removed");
        }
        if (fly.GetComponent<RandomFly>() != null) {
            randomFlyCount -= 1;
            Debug.Log("random Fly removed");
        }
        Destroy(fly);
    }

}
