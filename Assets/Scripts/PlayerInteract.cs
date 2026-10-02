using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Transform holdPoint;
    private GameObject heldItem = null; //Keeping track of currently held item

    public void OnInteractItem(InputValue value)
    {
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
        //Find nearby item

        //If item found
        //move item to holdpoint
        // set parent
        // disable item's rigidbody/colliders if physics not needed
    }

    void DropItem() 
    {
        //unparent
        //re enable physics
        //clear reference
        heldItem = null;
    }
}
