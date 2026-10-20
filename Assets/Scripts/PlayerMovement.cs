using System;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerMovement : MonoBehaviour
{
    private Vector2 movementInput;
    //[SerializeField]
    public float moveSpeed = 2f;
    public float rotationSpeed = 5f;
    private Rigidbody rb;
    public Vector3 movement;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Converting 2D movement to 3D
        // movement = new Vector3(movementInput.x, 0f, movementInput.y);
        
        if (movement != Vector3.zero)
        {
            //Moving object relative to world space
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            //using slerp for smoothly rotating towards target location
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed * rotationSpeed));
        }
        //
        //
        // transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        // rb.Move(movement, targetRotation);

    }

    private void FixedUpdate()
    {
        // Quaternion targetRotation = Quaternion.LookRotation(movement);
        Vector3 myMove = new Vector3(movementInput.x * moveSpeed * Time.deltaTime,0f,
            movementInput.y * moveSpeed * Time.deltaTime);
        Quaternion targetRotation = Quaternion.LookRotation(myMove);
        rb.Move(transform.position + myMove, targetRotation);
    }

    void OnMove(InputValue value)
    {
        //WASD
        movementInput = value.Get<Vector2>();
    }

    void moveCharacter(Vector3 direction)
    {
        rb.linearVelocity = direction * moveSpeed * Time.fixedDeltaTime;
    }
}
