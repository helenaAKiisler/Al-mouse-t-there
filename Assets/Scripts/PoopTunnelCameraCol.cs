using UnityEngine;

public class PoopTunnelCameraCol : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boat"))
        {
            //PoopTunnel active
            other.GetComponentInChildren<SwitchCam>().isPoopTunnel = true;
            Debug.Log("Poop Tunnel Entered");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //PoopTunnel inactive
        other.GetComponentInChildren<SwitchCam>().isPoopTunnel = false;
        Debug.Log("Poop Tunnel Exited");
    }
}
