using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    //Sorry I'm removing this because I have a quicker solution that probably won't be as janky, if it creates problems down the road we can come back to this. (squid-West)
    //I'm adding CineMachine btw

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
