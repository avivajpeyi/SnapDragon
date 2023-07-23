

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class FlyData : ScriptableObject
{
    public BaseFly.FlyType type;
    public int count;
    public int spawnMax;
    public GameObject prefab;
}