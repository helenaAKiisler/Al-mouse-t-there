using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField] private Transform objectHoldPointTransform; // Where the item will be held
    private ObjectGrabbable objectGrabbable; //Keeping track of currently held item
    public float interactRange = 2f;
    public LayerMask interactableLayer;
    private Collider playerCol;
    private SewerLid currenSewerLid;

    private void Start()
    {
        playerCol = GetComponent<Collider>();
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
        }
        else
        {
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
