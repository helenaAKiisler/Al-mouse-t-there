using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Transform holdPoint;
    private GameObject heldItem = null; //Keeping track of currently held item
    public float interactRange = 10;
    public LayerMask interactableLayer;

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
        if (heldItem== null)
        {
            TryPickUp();
        }
        else
        {
            DropItem();
        }
    }

    void TryPickUp() 
    {
        //Had to adjust offset origin upward, so that the ray casts from the waist height
        Vector3 rayOrigin = transform.position + Vector3.down * 0.2f;
        float sphereRadius = 0.5f; //Gives ray physical thickness

        RaycastHit hitData;
        //Find nearby item
        if (Physics.SphereCast(rayOrigin, sphereRadius, transform.forward, out hitData, interactRange, interactableLayer))
        {
            Debug.Log("Hit object: " + hitData.collider.name);
            //check if  object hit has the "interactable" tag
            heldItem = hitData.collider.gameObject;

            // Disable colliders so it doesn't collide with the player while held
            if (heldItem.TryGetComponent<Collider>(out Collider col))
            {
                col.enabled = false;
            }

            //Disable rigidbody and velocity
            if (heldItem.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true;
            }

            //Parenting hold object to hold point
            heldItem.transform.SetParent(holdPoint);
            heldItem.transform.localPosition = Vector3.zero; //Snaps to holdpoint option
            heldItem.transform.localRotation = Quaternion.identity;

        }
    }

    void DropItem() 
    {
        //re-enabling colliders
        if (heldItem.TryGetComponent<Collider>(out Collider col))
        {
            col.enabled = true;
        }

        //re-enable Rigidbody physics
        if (heldItem.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.isKinematic = false;
        }
        //clear reference and
        //unparent
        heldItem.transform.SetParent(null);
        heldItem = null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + transform.forward + Vector3.down * 0.2f, 0.5f);
    }
}
