using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class FlyFactory : Singleton<FlyFactory>
{
    public int framesPerSpawn;
    public List<FlyData> FlyDataList = new List<FlyData>();


    private int frameCount;
    [SerializeField]
    private Bounds bounds;


    void Start()
    {
        // if bounds are all zero, warn the user
        if (bounds.size == Vector3.zero)
        {
            Debug.LogError("FlyFactory has no bounds (set in the inspector)");
        }
        framesPerSpawn = 540;
        frameCount = 0;

        foreach (var flyDat in FlyDataList)
        {
            flyDat.count = 0;
        }
        
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
            if (Random.value < (1 - flyDat.count / flyDat.spawnMax))
            {
                flyDat.count++;
                CreateFly(flyDat.prefab);
            }
        }
    }

    public GameObject CreateFly(GameObject flyPrefab)
    {
        GameObject instantiatedFly = Instantiate(
            flyPrefab,
            RandomPointInBounds(),
            transform.rotation
        );
        return instantiatedFly;
    }




    private Vector3 RandomPointInBounds()
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            0
        );
    }


    public void destroyFly(GameObject flyG0)
    {
        BaseFly fly = flyG0.GetComponent<BaseFly>();

        if (fly != null)
        {
            foreach (var flyData in FlyDataList)
            {
                if (flyData.type == fly.type)
                {
                    flyData.count -= 1;
                    Debug.Log("Fly removed");
                }
            }
        }

        Destroy(fly.transform.root.gameObject);
    }


    private void OnDrawGizmos()
    {
        // Shade in bounds area
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(bounds.center, bounds.size);
    }
}