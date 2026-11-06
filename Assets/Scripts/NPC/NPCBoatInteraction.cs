using UnityEngine;
using UnityEngine.InputSystem;

public class NPCBoatInteraction : MonoBehaviour
{
    public GameObject NPC;
    private ObjectDockable objectDockable;
    public Boat boat;
    public ObjectDockable dock;
    private GameObject dockObject;

    public bool onBoat = false;

    public float interactRange = 2f;
    public LayerMask interactableLayer;
    public bool seesDock = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NPC = gameObject;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Dock"))
        {
            seesDock = true;
            dock = collision.GetComponentInParent<ObjectDockable>();

            if (dock != null && dock.HasBoat)
            {
                boat = dock.boat.GetComponent<Boat>();
                if (boat != null)
                {

                    boat.MountNPC(NPC);
                    onBoat = true;
                    print("Dock entered");
                }
            }

        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Dock"))
        {
            seesDock = false;
            print("Dock exited");
        }
    }
}
