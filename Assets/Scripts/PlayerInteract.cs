using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Transform holdPoint;
    private GameObject heldItem = null; //Keeping track of currently held item
    public float interactRange = 4f;
    public float sphereRadius = 0.5f; // Felt too punishing for ray to be so thin
    public LayerMask interactableLayer;

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
        //Casting ray forward from player's pos
        Ray ray = new Ray(holdPoint.position, holdPoint.forward);
        RaycastHit hit;

        //Find nearby item
        Debug.DrawRay(holdPoint.position, holdPoint.forward * interactRange, Color.red, 2f);
        if (Physics.SphereCast(ray,sphereRadius, out hit, interactRange, interactableLayer))
        {
            Debug.Log("Hit object: " + hit.collider.name);
            //check if  object hit has the "interactable" tag
            heldItem = hit.collider.gameObject;

            //Parenting hold object to hold point
            heldItem.transform.SetParent(holdPoint);
            heldItem.transform.localPosition = Vector3.zero; //Snaps to holdpoint option
            heldItem.transform.localRotation = Quaternion.identity;

            //Disable rigidbody/colliders
            if (heldItem.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.isKinematic = true;
            }

        }
    }

    void DropItem() 
    {
        //unparent
        heldItem.transform.SetParent(null);
        //re enable physics
        if (heldItem.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.isKinematic = false;
        }
        //clear reference
        heldItem = null;
    }
}
