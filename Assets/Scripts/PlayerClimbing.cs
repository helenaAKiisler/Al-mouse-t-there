using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerClimbing : MonoBehaviour
{
    private Vector2 movementInput;
    //[SerializeField]
    public float climbSpeed = 2.5f;
    private Rigidbody rb;
    public Vector3 movement;
    public bool isClimbing;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isClimbing = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        PlayerMovement player = GetComponent<PlayerMovement>();
        if (player != null)
        {
            if (isClimbing && !player.isInBoat)
            {
                //Climb direction
                Vector3 climb = new Vector3(movementInput.x, movementInput.y * climbSpeed, 0f);
                //up up and away
                rb.MovePosition(transform.position + climb * Time.fixedDeltaTime);
            }
        }
    }
    void OnMove(InputValue value)
    {
        //WASD
        movementInput = value.Get<Vector2>();
    }

    void moveCharacter(Vector3 direction)
    {
        rb.linearVelocity = direction * climbSpeed * Time.fixedDeltaTime;
    }
}
