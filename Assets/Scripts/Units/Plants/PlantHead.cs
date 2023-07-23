using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantHead : MonoBehaviour
{

    WordArt wordArtController;
    PlantController plantController;
    [SerializeField] protected AudioClip flyEatenFX;
    [SerializeField] protected GameObject flyEatenParticles;

    private void Start()
    {
        wordArtController = Resources.Load<GameObject>("WordArtController").GetComponent<WordArt>();
        plantController = GetComponentInParent<PlantController>();
        flyEatenFX = Resources.Load<AudioClip>("FlyEatenAudio");
        flyEatenParticles = Resources.Load<GameObject>("FlyEatenFx");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Dragon collides with " + other.gameObject.name);
        if (other.gameObject.CompareTag("Plant"))
        {
            Debug.Log("Make out time!");
            plantController.ResetPosition();
        }
        else if (other.gameObject.CompareTag("Fly"))
        {
            plantController.OnFlyEaten(other.gameObject.GetComponent<BaseFly>().flyValue);
            AudioSource.PlayClipAtPoint(flyEatenFX, transform.position);
            Instantiate(flyEatenParticles, transform.position, Quaternion.identity);

            BaseFly fly = other.gameObject.GetComponent<BaseFly>();
            fly.OnEaten();
             wordArtController.spawnWordArt(WordArt.WordArtTypes.Nom,transform.position,3);
            if (fly.type == BaseFly.FlyType.Winning)
            {
                GameManager.Instance.ChangeState(GameState.GameOver);
            }
        }
    }
}