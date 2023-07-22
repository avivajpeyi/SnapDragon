using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantHead : MonoBehaviour
{
    PlantController plantController;
    
    private void Start() {
        plantController = GetComponentInParent<PlantController>();
    } 
    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log(other);
         if (other.gameObject.CompareTag("Plant"))
        {
            Debug.Log("Snap!");
            plantController.ResetPosition();
        }
    }
}
