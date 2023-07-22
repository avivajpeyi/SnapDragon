

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class FlyData : ScriptableObject
{
    public BaseFly.FlyType type;
    public float spawnFrequency;
    public float spawnProb;
    public int count;
    public int spawnMax;
    public GameObject prefab;
}