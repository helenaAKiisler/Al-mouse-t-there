using Unity.Cinemachine;
using UnityEngine;

public class SwitchCam : MonoBehaviour
{
    public CinemachineCamera playerCam;
    public CinemachineCamera boatCam;

    private PlayerMovement player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isInBoat)
        {
            CameraManager.SwitchCamera(boatCam);
        }
        else
        {
            CameraManager.SwitchCamera(playerCam);
        }
    }
}
