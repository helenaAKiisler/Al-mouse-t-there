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
    private GameObject playerObject;
    
    [SerializeField] public Transform playerSlot;
    private PlayerInput boatInput;
    public GameObject boatObject;
    public  Transform boatTransform;
    public GameObject player;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boatRigidbody =  GetComponent<Rigidbody>();
        boatInput = GetComponent<PlayerInput>();
        boatObject = gameObject;
        boatTransform = boatObject.transform;
        
        boatInput.enabled = false;
        MountBoat(player);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void FixedUpdate()
    {
        Vector3 pos = transform.position;
        pos.y = WaterLevels.Instance.transform.position.y;
        transform.position = pos;
        
        if (!PlayerMounted)
        {
            if (boatObject.GetComponentInParent<ObjectDockable>() != null)
            {
                boatRigidbody.position = boatObject.GetComponentInParent<ObjectDockable>().GetBoatLocation().position;
            }
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
        playerTransform.localPosition = Vector3.zero;
        playerTransform.localRotation = Quaternion.identity;
        print("still locking player");
    }

    void OnMove(InputValue value)
    {
        //WASD
        movementInput = value.Get<Vector2>();
        // Debug.Log("Movement input: " + movementInput);
    }

    void moveCharacter(Vector3 direction)
    {
        boatRigidbody.linearVelocity = direction * moveSpeed * Time.fixedDeltaTime;
    }

    public void MountBoat(GameObject player)
    {
        // Get the player's components
        playerObject = player;
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
        playerTransform.SetParent(playerSlot, false);
        playerTransform.localPosition = Vector3.zero;
        playerTransform.localRotation = Quaternion.identity;
        
        //Disabling player's playerInput
        PlayerInput playerInput = player.GetComponent<PlayerInput>();

        if (playerInput != null)
        {
            playerInput.enabled = false;
        }

        boatInput.enabled = true;
        
        // Enable boat movement
        PlayerMounted = true;
        boatTransform.localScale = new Vector3(1,0.2f,2);

        print("Player Mounted: " + PlayerMounted);
    }

    public void OnMount(InputValue value)
    {
        PlayerBoatInteraction PBI = playerObject.GetComponent<PlayerBoatInteraction>();
        print("dismounting");
        if (!(PBI.seesDock))
        {
            return;
        }
        PBI.dock.Dock(boatObject);
        DisMountBoat(PBI);
        
    }
    
    public void DisMountBoat(PlayerBoatInteraction PBI)
    {
        playerMovement = playerObject.GetComponent<PlayerMovement>();
        playerTransform = playerObject.transform;
        //playerObject = player;
        
        boatInput.enabled = false;
        
        playerTransform.SetParent(null, false);
        playerTransform.position = PBI.dock.playerLocation.position;
        playerTransform.rotation = Quaternion.identity;
        
        PlayerInput playerInput = playerObject.GetComponent<PlayerInput>();
        
        playerInput.enabled = true;
        playerMovement.enabled = true;
        
        PlayerMounted = false;
        boatTransform.localScale = new Vector3(1,0.2f,1);
    }
}
