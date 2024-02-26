using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ActiveCamera : MonoBehaviour
{
    public int cameraNum;
    public CameraController cameraController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("player")){ 
             setActiveCamera(cameraNum);
             cameraController.switchCamera(cameraNum); 
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            GameManager.instance.weaponChange = true;
        }
    }

    public void setActiveCamera(int _cameraNum)
    {
        GameManager.instance.activeCamera = _cameraNum;
    }
}
