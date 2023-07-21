// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class FlySpawner : MonoBehaviour
// {
//     public float spawnInterval = 1f; // Time interval between spawning flies
//     public GameObject flyPrefab;    // The prefab of the fly object to spawn
//     public Transform spawnArea;     // (Optional) The transform representing the spawn area

//     private float timeSinceLastSpawn;

//     void Start()
//     {
//         timeSinceLastSpawn = spawnInterval;
//     }

//     void Update()
//     {
//         // Update the time since last spawn
//         timeSinceLastSpawn += Time.deltaTime;

//         // Check if it's time to spawn a new fly
//         if (timeSinceLastSpawn >= spawnInterval)
//         {
//             SpawnFly();
//             timeSinceLastSpawn = 0f;
//         }
//     }

//     void SpawnFly()
//     {
//         // Check if a fly prefab is assigned
//         if (flyPrefab == null)
//         {
//             Debug.LogWarning("FlyPrefab is not assigned in the FlySpawner!");
//             return;
//         }

//         // Spawn the fly at the spawn area or at the spawner's position if spawnArea is not specified
//         Vector3 spawnPosition = (spawnArea != null) ? GetRandomPositionInSpawnArea() : transform.position;

//         // Instantiate the fly object
//         GameObject newFly = Instantiate(flyPrefab, spawnPosition, Quaternion.identity);

//         // Ensure that the new fly object is set up correctly (e.g., assign it to a FlyBehavior script)
//         FlyBehavior flyBehavior = newFly.GetComponent<FlyBehavior>();
//         if (flyBehavior != null)
//         {
//             flyBehavior.fly = CreateFly();
//         }
//         else
//         {
//             Debug.LogWarning("FlyBehavior script not found on the fly prefab!");
//         }
//     }

//     // (Optional) If using a spawn area, get a random position within the spawn area
//     Vector3 GetRandomPositionInSpawnArea()
//     {
//         Vector3 randomPosition = spawnArea.position;
//         randomPosition.x += Random.Range(-spawnArea.localScale.x / 2f, spawnArea.localScale.x / 2f);
//         randomPosition.y += Random.Range(-spawnArea.localScale.y / 2f, spawnArea.localScale.y / 2f);
//         return randomPosition;
//     }

//     // (Optional) Create a new fly using the FlyFactory2D or FlyFactory3D (based on your project setup)
//     BaseFly CreateFly()
//     {
//         // Use the appropriate factory to create the desired fly type
//         FlyFactory2D factory2D = new FlyFactory2D();
//         return factory2D.CreateFly(FlyType.Slow); // For example, spawn a slow 2D fly
//     }
// }

