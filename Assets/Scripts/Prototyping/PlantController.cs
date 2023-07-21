using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : MonoBehaviour
{

    public float RotateSpeed = 100f;
    public float growthRate = 0.1f;
    public float maxDist = 1.0f;

    LineRenderer neck;
    public Transform plantHead;
        
    // Start is called before the first frame update
    void Start()
    {
        neck = gameObject.AddComponent<LineRenderer>();
        neck.SetPosition(0, this.transform.position);
        neck.SetPosition(1, plantHead.position);
        neck.material = new Material(Shader.Find("Sprites/Default"));
        Color c1 = Color.white;
        neck.SetColors(c1, c1);
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        // if the player presses the space bar, grow the plant
        if (Input.GetKey(KeyCode.Space))
        {
            Grow();
        }
        else
        {
            Reset();
        }
        
        
        // if the player presses the left or right arrow keys, rotate the plant around the z axis
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(Vector3.forward * -RotateSpeed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(Vector3.forward * RotateSpeed * Time.deltaTime);
        
        neck.SetPosition(0, this.transform.position);
        neck.SetPosition(1, plantHead.position);
    }

    
    
    
    

    public void Grow()
    {
        
        
        // move plant head local position forward until it reaches maxDist
        if (plantHead.localPosition.y < maxDist)
        {
            plantHead.localPosition += new Vector3(0, growthRate, 0);
        }


       
        

    }
    
    public void Reset()
    {
        // back to original position
        plantHead.localPosition = new Vector3(0, 0, 0);
        neck.SetPosition(0, this.transform.position);
        neck.SetPosition(1, plantHead.position);
    }

}
