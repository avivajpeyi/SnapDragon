using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordArtController : MonoBehaviour
{
    public List<WordArt> wordArtList;


    public void spawnWordArt(Vector3 position, WordArtTypes type)
    {
        WordArt wordArt = wordArtList.Find(x => x.type == type);
        GameObject wordArtObject =
            Instantiate(wordArt.prefab, position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(wordArt.sfx, position);
        Destroy(wordArtObject, 0.5f);
    }


    public void SpawnRandomWordArt(Vector3 position)
    {
        int randomIndex = Random.Range(0, wordArtList.Count-1); // Dont include smooch
        WordArt wordArt = wordArtList[randomIndex];
        spawnWordArt(position, wordArt.type);
    }


    public enum WordArtTypes
    {
        Chomp,
        Nom,
        Snap,
        Yum,
        Smooch
    }
}