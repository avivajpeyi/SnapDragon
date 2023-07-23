using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordArt : MonoBehaviour
{

    public GameObject chompObject;
    public GameObject nomObject;
    public GameObject snapObject;
    public GameObject yumObject;

    public Dictionary<WordArtTypes,GameObject> wordArtMap;
    // {
    //     {WordArtTypes.Chomp,chompObject},
    //     {WordArtTypes.Nom,nomObject},
    //     {WordArtTypes.Snap,snapObject},
    //     {WordArtTypes.Yum,yumObject}
    // };


    void Start() {
        wordArtMap = new Dictionary<WordArtTypes, GameObject>();
        wordArtMap.Add(WordArtTypes.Chomp,chompObject);
        wordArtMap.Add(WordArtTypes.Nom,nomObject);
        wordArtMap.Add(WordArtTypes.Snap,snapObject);
        wordArtMap.Add(WordArtTypes.Yum,yumObject); 
        foreach (var item in wordArtMap.Keys)
        {
            Debug.Log("We have: " + item);
        }
    }   

    public void spawnWordArt(WordArtTypes art, Vector3 position, float time) {
        Debug.Log(wordArtMap.Keys);
        GameObject summonedArt = Instantiate(wordArtMap[art],position,transform.rotation);
        Destroy(summonedArt, time);
    }

    public enum WordArtTypes
    {
        Chomp,
        Nom,
        Snap,
        Yum
    }
}
