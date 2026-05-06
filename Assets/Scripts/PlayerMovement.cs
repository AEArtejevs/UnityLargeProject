using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float sprintSpeed = 8f;

    [Header("Jumping")]
    public float jumpForce = 5f;
    public float groundCheckDistance = 1.1f;

    private Rigidbody rb;
    private Vector3 movement;
    private bool isSprinting;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector2 input = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
            input.y += 1;

        if (Keyboard.current.sKey.isPressed)
            input.y -= 1;

        if (Keyboard.current.aKey.isPressed)
            input.x -= 1;

        if (Keyboard.current.dKey.isPressed)
            input.x += 1;

        movement = new Vector3(input.x, 0f, input.y).normalized;
        isSprinting = Keyboard.current.leftShiftKey.isPressed;

        if (Keyboard.current.spaceKey.wasPressedThisFrame && IsGrounded())
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = transform.right * movement.x + transform.forward * movement.z;
        float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;

        rb.linearVelocity = new Vector3(
            moveDirection.x * currentSpeed,
            rb.linearVelocity.y,
            moveDirection.z * currentSpeed
        );
    }
    bool IsGrounded()
    {
        // It shoots an invisible line downward from the player.
        // If it hits something under the player, player is grounded.
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
    }
    void Jump()
    {
        // First line resets vertical speed before jumping.
        // Second line adds an instant upward push.
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    
}
