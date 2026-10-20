using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    private Rigidbody objectRigidbody;
    private Transform objectHoldPointTransform;

    private void Awake()
    {   
        //grab the rigidbody reference
        objectRigidbody = GetComponent<Rigidbody>();
    }
    public void Grab(Transform objectHoldPointTransform) 
    {
        //updating hold point
        this.objectHoldPointTransform = objectHoldPointTransform;
        objectRigidbody.useGravity = false;
    }

    public void Drop()
    {
        this.objectHoldPointTransform = null;
        objectRigidbody.useGravity = true;
    }

    private void FixedUpdate()
    {   
        //We have an object we're holding
        if (objectHoldPointTransform != null)
        {
            float lerpSpeed = 10f;
            //For smoothening the object's movement while holding between these 2 positions
            Vector3 newPos = Vector3.Lerp(transform.position, objectHoldPointTransform.position, Time.deltaTime * lerpSpeed);
            objectRigidbody.MovePosition(newPos);
        }
    }
}
