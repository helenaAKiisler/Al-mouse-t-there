using Unity.Cinemachine;
using UnityEngine;

public class SwitchCam : MonoBehaviour
{
    public CinemachineCamera playerCam;
    public CinemachineCamera boatCam;
    public CinemachineCamera boatCamLeft;

    private PlayerMovement player;
    public bool isPoopTunnel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<PlayerMovement>();
        isPoopTunnel = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isInBoat)
        {
            if (isPoopTunnel)
            {
                CameraManager.SwitchCamera(boatCamLeft);
            } else
            {
                CameraManager.SwitchCamera(boatCam);
            }
        }
        else
        {
            CameraManager.SwitchCamera(playerCam);
        }
    }
}
