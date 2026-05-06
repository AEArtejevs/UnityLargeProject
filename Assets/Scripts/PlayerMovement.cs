using UnityEngine;

// -------------------------------------------------------
// THIS SCRIPT USES THE OLD INPUT SYSTEM
// -------------------------------------------------------

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    [Header("Movement")]
    public float speed = 12f;
    public float sprintSpeed = 18f;

    [Header("Jumping")]
    public float jumpHeight = 3f;
    public float gravity = -9.81f * 2f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Vector3 velocity;
    private bool isSprinting;
    private bool isGrounded;
    private bool isMoving;

    private Vector3 lastPosition = Vector3.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        CheckGround();
        Sprint();
        MovePlayer();
        Jump();
        ApplyGravity();
        CheckMovement();
    }

    void CheckGround()
    {
        // Checks if the GroundCheck object is touching the ground layer.
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Keeps the player attached to the ground.
        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }
    }

    void MovePlayer()
    {
        // Old Input System movement input.
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Prevents diagonal movement from being faster.
        Vector3 inputDirection = new Vector3(x, 0f, z).normalized;

        // Converts input direction to player-facing direction.
        Vector3 moveDirection = transform.right * inputDirection.x + transform.forward * inputDirection.z;

        // Uses sprint speed if sprinting, otherwise normal speed.
        float currentSpeed = isSprinting ? sprintSpeed : speed;

        // Moves the player horizontally.
        controller.Move(moveDirection * currentSpeed * Time.deltaTime);
    }

    void Sprint()
    {
        // Hold Left Shift to sprint.
        isSprinting = Input.GetKey(KeyCode.LeftShift);
    }

    void Jump()
    {
        // Jump only once when Space is pressed and player is grounded.
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void ApplyGravity()
    {
        // Applies gravity every frame.
        velocity.y += gravity * Time.deltaTime;

        // Moves the player vertically.
        controller.Move(velocity * Time.deltaTime);
    }

    void CheckMovement()
    {
        // Checks if player position changed while grounded.
        isMoving = lastPosition != transform.position && isGrounded;

        lastPosition = transform.position;
    }
}