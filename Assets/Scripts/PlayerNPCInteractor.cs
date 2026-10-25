using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerNPCInteractor : MonoBehaviour
{
    public NPC currentNpc;

    private void OnTriggerEnter(Collider other)
    {
        NPC npc = other.gameObject.GetComponentInParent<NPC>();
        if (npc != null)
        {
            currentNpc = npc;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        NPC npc = other.gameObject.GetComponentInParent<NPC>();
        if (npc != null && currentNpc == npc)
        {
            currentNpc = null;
        }
    }
    public void OnTalk(InputValue value)
    {
        if(currentNpc != null)
            currentNpc.Interact();
    }
}
