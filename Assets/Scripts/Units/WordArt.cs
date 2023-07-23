

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class WordArt: ScriptableObject
{
    
    public WordArtController.WordArtTypes type;
    public AudioClip sfx;
    public GameObject prefab;
}
