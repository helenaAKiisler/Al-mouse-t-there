using UnityEngine;
using UnityEngine.InputSystem;

public class Boat : MonoBehaviour
{
    public bool PlayerMounted = false;
    private PlayerMovement playerMovement;
    private Rigidbody boatRigidbody;
    private Vector2 movementInput;
    public float moveSpeed = 2f;
    public float rotationSpeed = 5f;
    public Transform playerTransform;
    
    [SerializeField] public Transform playerSlot;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boatRigidbody =  GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void FixedUpdate()
    {
        if (!PlayerMounted)
        {
            return;
        }
        
        //Calculating movement direction
        Vector3 myMove = new Vector3(movementInput.x, 0f,
            movementInput.y);

        //When moving...
        //Checking movementInput since that's  what gets updated by input system
        if (movementInput != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(myMove);
            //Separate rotation and movement
            //using slerp for smoothly rotating towards target location
            boatRigidbody.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * moveSpeed * rotationSpeed));
            boatRigidbody.MovePosition(transform.position + myMove * (Time.fixedDeltaTime * moveSpeed));
        }
    }

    void OnMove(InputValue value)
    {
        //WASD
        movementInput = value.Get<Vector2>();
    }

    void moveCharacter(Vector3 direction)
    {
        boatRigidbody.linearVelocity = direction * moveSpeed * Time.fixedDeltaTime;
    }

    public void MountBoat(GameObject player)
    {
        // Get the player's components
        playerMovement = player.GetComponent<PlayerMovement>();
        playerTransform = player.transform;

        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement component not found on player!");
            return;
        }

        // Disable normal player movement
        playerMovement.enabled = false;

        // Attach player to the boat
        playerTransform.SetParent(transform, false);

        // Position the player at the seat
        playerTransform.position = playerSlot.position;
        playerTransform.rotation = playerSlot.rotation;

        // Enable boat movement
        PlayerMounted = true;
    }
}
