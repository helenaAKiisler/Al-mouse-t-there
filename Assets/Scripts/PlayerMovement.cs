using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerMovement : MonoBehaviour
{
    private Vector2 movementInput;
    //[SerializeField]
    public float moveSpeed = 2f;
    public float rotationSpeed = 5f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Converting 2D movement to 3D
        Vector3 moveDirection = new Vector3(movementInput.x, 0f, movementInput.y);

        if (moveDirection != Vector3.zero)
        {
            //Moving object relative to world space
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            //using slerp for smoothly rotating towards target location
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed * rotationSpeed);
        }

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

    }

    void OnMove(InputValue value)
    {
        //WASD
        movementInput = value.Get<Vector2>();
    }

}
