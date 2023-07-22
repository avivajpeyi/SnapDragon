using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlyFactory : Singleton<FlyFactory>
{
    public int framesPerSpawn;
    public List<FlyData> FlyDataList = new List<FlyData>();


    private int frameCount;
    private float sceneWidth;
    private float sceneHeight;


    void Start()
    {
        CalculateScreneSize();
        framesPerSpawn = 540;
        frameCount = 0;
        for (int i = 0; i < 3; i++)
            Spawn();
    }


    void Update()
    {
        frameCount++;
        if (frameCount >= framesPerSpawn)
        {
            Spawn();
            frameCount = 0;
        }
    }

    void Spawn()
    {
        foreach (var flyDat in FlyDataList)
        {
            if (Random.value < flyDat.spawnProb)
            {
                flyDat.count++;
                CreateFly(flyDat.prefab);
            }
        }
    }

    public GameObject CreateFly(GameObject flyPrefab)
    {
        Debug.Log("Prefab: " + flyPrefab);
        Debug.Log("PrefabName" + flyPrefab.name);
        GameObject instantiatedFly = Instantiate(
            flyPrefab,
            generatePosition(),
            transform.rotation
        );
        return instantiatedFly;
    }

    private void CalculateScreneSize()
    {
        float halfHeight = Camera.main.orthographicSize;
        float halfWidth = halfHeight * Camera.main.aspect;
        sceneWidth = halfWidth * 2f;
        sceneHeight = halfHeight * 2f;
    }


    private Vector3 generatePosition()
    {
        float xPos = Random.value * (sceneWidth) - sceneWidth / 2;
        float yPos = Random.value * (sceneHeight) - sceneHeight / 2;
        return new Vector3(xPos, yPos, 0);
    }

    public void destroyFly(GameObject flyG0)
    {
        BaseFly fly = flyG0.GetComponent<BaseFly>();

        if (fly != null)
        {
            foreach (var flyData in FlyDataList)
            {
                if (flyData.type == fly.flyType)
                {
                    flyData.count -= 1;
                    Debug.Log("Fly removed");
                }
            }
        }

        Destroy(fly.transform.root.gameObject);
    }
}