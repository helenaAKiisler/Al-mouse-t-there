using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerNPCInteractor : MonoBehaviour
{
    public NPC currentNpc;

    private void OnTriggerEnter(Collider other)
    {
        NPC npc = other.GetComponentInParent<NPC>();
        if (npc == null) return;

        currentNpc = npc;
        npc.OnPlayerEnter();
    }

    private void OnTriggerExit(Collider other)
    {
        NPC npc = other.GetComponentInParent<NPC>();
        if (npc == null || npc != currentNpc) return;

        currentNpc = null;
        npc.OnPlayerExit();
    }

    public void OnTalk(InputValue value)
    {
        if (currentNpc != null)
            currentNpc.Interact();
    }
}
