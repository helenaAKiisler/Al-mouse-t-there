using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerMovement : MonoBehaviour
{
    private Vector2 movementInput;
    //[SerializeField]
    public float moveSpeed = 2f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Converting 2D movement to 3D
        Vector3 moveDirection = new Vector3(movementInput.x, 0f, movementInput.y);

        //Moving object relative to world space
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }

    void OnMove(InputValue value)
    {
        //WASD
        movementInput = value.Get<Vector2>();
    }

}
