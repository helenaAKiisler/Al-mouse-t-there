using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField] private Transform objectHoldPointTransform; // Where the item will be held
    private ObjectGrabbable objectGrabbable; //Keeping track of currently held item
    public float interactRange = 2f;
    public LayerMask interactableLayer;

    [SerializeField] private PlayerNPCInteractor npcInteractor;
    public ObjectGrabbable HeldObject => objectGrabbable;
    
    
    private void Awake()
    {
        if (npcInteractor == null)
            npcInteractor = GetComponentInParent<PlayerNPCInteractor>();
    }
    
    // Called by the NPC when it takes the item
    public ObjectGrabbable TakeHeldObject()
    {
        ObjectGrabbable held = objectGrabbable;
        objectGrabbable = null;
        return held;
    }
    private void Start()
    {
    }

    private void Update()
    {
    }

    public void OnInteract(InputValue value)
    {
        Debug.Log("Interact button pressed!");
        //Checking button pressed down
        if (objectGrabbable == null)
        {
            //If the player isn't holding an object, they can try grab
            TryPickUp();
            return;
        }
        //Currently holding something that the npc wants
        NPC npc = npcInteractor != null ? npcInteractor.currentNpc : null;
        if (npc != null && npc.TryReceiveItem(this)) return;
        //Currently holding something
        objectGrabbable.Drop();
        objectGrabbable = null;
    
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
                objectGrabbable.Grab(objectHoldPointTransform);
                Debug.Log(objectGrabbable);
            }



        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + transform.forward + Vector3.down * 0.2f, 0.6f);
    }
}
