using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantHead : MonoBehaviour
{
    PlantController plantController;
    protected AudioClip flyEatenFX;
    protected GameObject flyEatenParticles;

    private void Start()
    {
        plantController = GetComponentInParent<PlantController>();
        flyEatenFX = Resources.Load<AudioClip>("FlyEatenAudio");
        flyEatenParticles = Resources.Load<GameObject>("FlyEatenFx");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Fly"))
        {
            plantController.OnFlyEaten(other.gameObject.GetComponent<BaseFly>().flyValue);
            AudioSource.PlayClipAtPoint(flyEatenFX, transform.position);
            Instantiate(flyEatenParticles, transform.position, Quaternion.identity);

            BaseFly fly = other.gameObject.GetComponent<BaseFly>();
            fly.OnEaten();
            if (fly.type == BaseFly.FlyType.Winning)
            {
                GameManager.Instance.ChangeState(GameState.GameOver);
            }
        }
    }
}