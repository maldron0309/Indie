using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform personaje;

    private float sizeCamera;
    private float heightScreen;

    // Start is called before the first frame update
    void Start()
    {
        sizeCamera = Camera.main.orthographicSize;
        heightScreen = sizeCamera * 2;
    }

    // Update is called once per frame
    void Update()
    {
        CalculatorPositionCamera(); 
    }

    void CalculatorPositionCamera()
    {
        int screenCharacter = Mathf.FloorToInt(personaje.position.y / heightScreen); 
        float heightCamera = (screenCharacter * heightScreen) + sizeCamera;
        transform.position = new Vector3(transform.position.x, heightCamera, transform.position.z);
    }
}
