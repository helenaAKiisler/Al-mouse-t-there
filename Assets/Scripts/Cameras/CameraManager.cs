using UnityEngine;
using Unity.Cinemachine;
using System.Collections.Generic;

public class CameraManager : MonoBehaviour
{
    //List of cinemachine cameras
    static List<CinemachineCamera> cameras = new List<CinemachineCamera>();

    public static CinemachineCamera activeCamera = null; //currently active camera


    public static void SwitchCamera(CinemachineCamera newCam)//pass new camera
    {
        newCam.Priority = 10;//update priority
        activeCamera = newCam;//store reference

        foreach (CinemachineCamera cam in cameras)
        {//check all cameras in scene
            if (cam != newCam) {//ignore active camera
                cam.Priority = 0;//set others to 0
            }
        }
    }

    public static void Register(CinemachineCamera cam)
    {
        cameras.Add(cam);
    }

    public static void Unregister(CinemachineCamera cam)
    {
        cameras.Remove(cam);
    }

    //Checking if current cam is activeCam
    public static bool IsActiveCamera(CinemachineCamera cam)
    {
        return cam == activeCamera;
    }
}
