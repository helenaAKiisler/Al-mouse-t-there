using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerNPCInteractor : MonoBehaviour
{
    public NPC currentNpc;

    public void OnTalk(InputValue value)
    {
        if(currentNpc != null)
            currentNpc.Interact();
    }
}
