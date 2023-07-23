using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class FlyFactory : MonoBehaviour
{
    public bool canSpawn = false;

    private void Awake() => GameManager.OnBeforeStateChanged += OnStateChanged;

    private void OnDestroy() => GameManager.OnBeforeStateChanged -= OnStateChanged;


    public int framesPerSpawn;
    public List<FlyData> FlyDataList = new List<FlyData>();


    private int frameCount;
    [SerializeField] private Bounds bounds;
    private Coroutine spawnCoroutine;

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
    }


    void OnStateChanged(GameState newState)
    {
        if (newState == GameState.InGame)
        {
            canSpawn = true;
            Debug.Log("FlyFactory can spawn now");
            spawnCoroutine = StartCoroutine(SpawnFlies());
        }
            
        else
        {
            canSpawn = false;
            if (spawnCoroutine != null)
            {
                StopCoroutine(spawnCoroutine);
            }
        }

    }


    // Make an IEnumerator coroutine that spawns flies
    private IEnumerator SpawnFlies()
    {
        while (canSpawn)
        {
            Spawn();
            yield return new WaitForSeconds(0.5f);
        }

        
    }

    void Spawn()
    {
        foreach (var flyDat in FlyDataList)
        {
            for (int i=0; i<flyDat.spawnMax; i++)
            {
                if (flyDat.count < flyDat.spawnMax)
                {
                    Debug.Log("Spwing " + flyDat.type+ " Num SPawned " + flyDat.count + "/" + flyDat.spawnMax);
                    CreateFly(flyDat.prefab);
                    flyDat.count++;
                }
                    
            }

        }
    }

    public GameObject CreateFly(GameObject flyPrefab)
    {
        GameObject instantiatedFly = Instantiate(
            flyPrefab,
            RandomPointInBounds(),
            transform.rotation,
            transform
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
                }
            }

            Destroy(flyG0, 0.1f);
        }
    }


    private void OnDrawGizmosSelected()
    {
        // Shade in bounds area
        Gizmos.color = new Color(1, 0, 0, 0.1f);
        Gizmos.DrawCube(bounds.center, bounds.size);
    }
}