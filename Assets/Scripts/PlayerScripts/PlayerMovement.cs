using System;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerMovement : MonoBehaviour
{
    private Vector2 movementInput;
    //[SerializeField]
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;
    private Rigidbody rb;
    public Vector3 movement;
    public float fallForce = 5.0f;
    public bool isInBoat;
    private Animator animator;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isInBoat = false;
        animator = GetComponentInChildren<Animator>();
    }
   

    // Update is called once per frame - ui, key presses general timers
    void Update()
    {
    }

    //FixedUpdate for physics and movement
    private void FixedUpdate()
    {
        //If player is climbing, no moving
        PlayerClimbing player = GetComponent<PlayerClimbing>();
        if (player.isClimbing)
        {
            return;
        }

        if (!isInBoat)
        {
            //Calculating movement direction
            Vector3 myMove = new Vector3(movementInput.x, 0f,
            movementInput.y);
            float moveMagnitude = movementInput.magnitude;
            rb.AddForce(Vector3.down * fallForce * rb.mass,ForceMode.Force);
            //When moving...
            //Checking movementInput since that's  what gets updated by input system
            if (movementInput != Vector2.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(myMove);
                //Separate rotation and movement
                //using slerp for smoothly rotating towards target location
                rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * moveSpeed * rotationSpeed));
                rb.MovePosition(transform.position + myMove * Time.fixedDeltaTime * moveSpeed);
                
                //For animation
                //Input magnitude - key press up to 1.0, when released, down to 0
                animator.SetFloat("Speed", moveMagnitude * moveSpeed * 10);

            }
            else
            {
                animator.SetFloat("Speed", moveMagnitude * moveSpeed);
            }
        }
        else { return; }
    }

    void OnMove(InputValue value)
    {
        //WASD
        movementInput = value.Get<Vector2>();
        //Debug.Log("Movement input: " + movementInput);
    }

    void moveCharacter(Vector3 direction)
    {
        rb.linearVelocity = direction * moveSpeed * Time.fixedDeltaTime;
    }
}
