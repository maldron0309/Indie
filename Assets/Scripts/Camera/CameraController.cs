using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera[] cameras;

    public CinemachineVirtualCamera camera1;
    public CinemachineVirtualCamera camera2;
    public CinemachineVirtualCamera camera3;
    public CinemachineVirtualCamera camera4;
    public CinemachineVirtualCamera camera5;
    public CinemachineVirtualCamera camera6;
    public CinemachineVirtualCamera camera7;
    public CinemachineVirtualCamera camera8;
    public CinemachineVirtualCamera camera9;

    public CinemachineVirtualCamera startCamera;
    private CinemachineVirtualCamera currentCam;



    // Start is called before the first frame update
    void Start()
    {
        currentCam = startCamera;
        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] == currentCam)
            {
                cameras[i].Priority = 20;
            }
            else
            {
                cameras[i].Priority = 10;
            }
        }
    }

    public void switchCamera(int newCam)
    {

        currentCam = cameras[newCam];

        currentCam.Priority = 20;
        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] != currentCam)
            {
                cameras[i].Priority = 10;
            }
        }
    }
}
