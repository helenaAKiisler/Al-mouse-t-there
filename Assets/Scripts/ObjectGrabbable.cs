using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    private Rigidbody objectRigidbody;
    private Transform objectHoldPointTransform;

    private Collider objectCollider;
    private Collider playerCollider;

    private bool playerInteractable=false;
    [SerializeField] private HUD hud;
    public HUD Hud => hud;

    private void Awake()
    {   
        //grab the rigidbody reference
        objectRigidbody = GetComponent<Rigidbody>();
        objectCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        
    }
    public void Grab(Transform objectHoldPointTransform, Collider playerCol) 
    {
        //updating hold point
        this.objectHoldPointTransform = objectHoldPointTransform;
        objectRigidbody.useGravity = false;
        playerCollider = playerCol;

        //Ignore collision
        if (playerCollider != null && objectCollider != null)
        {
            Physics.IgnoreCollision(objectCollider, playerCollider, true);
        }
        hud.OnInteractEnd();
    }

    public void Drop()
    {
        if (playerCollider != null && objectCollider != null)
        {
            Physics.IgnoreCollision(objectCollider, playerCollider, false);
        }

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInteractable = true;
            hud.OnInteract();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInteractable = false;
            hud.OnInteractEnd();
        }
    }
}
