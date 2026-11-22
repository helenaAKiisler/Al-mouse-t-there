using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField] private Transform objectHoldPointTransform; // Where the item will be held
    [SerializeField] private PlayerNPCInteractor npcInteractor;   // auto-found if empty

    private ObjectGrabbable objectGrabbable; //Keeping track of currently held item
    public float interactRange = 2f;
    public LayerMask interactableLayer;
    private Collider playerCol;
    private SewerLid currenSewerLid;
    public ObjectGrabbable HeldObject => objectGrabbable;
    // [SerializeField] private HUD hud;
    
    private void Start()
    {
        playerCol = GetComponent<Collider>();
        if (npcInteractor == null)
            npcInteractor = GetComponentInParent<PlayerNPCInteractor>();
    }

    private void Update()
    {
    }
    
    public ObjectGrabbable TakeHeldObject()
    {
        ObjectGrabbable held = objectGrabbable;
        objectGrabbable = null;
        return held;
    }
    
    public void OnInteract(InputValue value)
    {
        Debug.Log("Interact button pressed!");
        //Checking button pressed down
        if (objectGrabbable == null)
        {
            //If the player isn't holding an object, they can try grab
            TryPickUp();
        }
        else
        {
            //If the nearby NPC wants it, Player hands it over instead of dropping
            NPC npc = npcInteractor != null ? npcInteractor.currentNpc : null;
            if (npc != null && npc.TryReceiveItem(this)) return;
            //Currently holding something
            objectGrabbable.Drop();
            //If it's an attachable
            if (objectGrabbable.TryGetComponent<ObjectAttachable>(out ObjectAttachable objectAttachable))
            {
                if (objectAttachable.canAttach)
                {
                    objectAttachable.Attach();
                }
            }
            objectGrabbable = null;
        }
    }

    void TryPickUp() 
    {
        //Had to adjust offset origin upward, so that the ray casts from the waist height
        Vector3 rayOrigin = transform.position + Vector3.down * 0.2f;
        float sphereRadius = 0.6f; //Gives ray physical thickness

        //Find nearby item
        if (Physics.SphereCast(rayOrigin, sphereRadius, transform.forward, out RaycastHit hitData, interactRange, interactableLayer))
        {
            Debug.Log("Hit object: " + hitData.collider.name);
            if (hitData.transform.TryGetComponent(out objectGrabbable))
            {
                objectGrabbable.Grab(objectHoldPointTransform, playerCol);
                Debug.Log(objectGrabbable);
            }



        }
        //Picking items up repeatedly
        if (objectGrabbable == null)
            TryPickUpOverlapping();
    }
    
    private void TryPickUpOverlapping()
    {
        Vector3 center = transform.position + transform.forward * 0.5f + Vector3.down * 0.2f;
        Collider[] hits = Physics.OverlapSphere(center, 0.8f, interactableLayer);

        foreach (Collider hit in hits)
        {
            ObjectGrabbable grabbable = hit.GetComponentInParent<ObjectGrabbable>();
            if (grabbable == null) continue;

            objectGrabbable = grabbable;
            objectGrabbable.Grab(objectHoldPointTransform, playerCol);
            return;
        }
    }

    //Reusing R key
    void OnTalk(InputValue value)
    {
        if (currenSewerLid != null && currenSewerLid.hasHandle)
        {
            currenSewerLid.OpenDoor();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + transform.forward + Vector3.down * 0.2f, 0.6f);
    }
}
