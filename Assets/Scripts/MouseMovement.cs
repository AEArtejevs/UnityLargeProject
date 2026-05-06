using UnityEngine;
using UnityEngine.InputSystem;

public class MouseMovement : MonoBehaviour
{
    [Header("Mouse Settings")]
    public float mouseSensitivity = 500f;

    [Header("Camera")]
    // public Transform cameraHolder;

    float xRotation = 0f;
    float yRotation = 0f;

    [Header("Clamping")]
    public float topClamp = -90f;
    public float bottomClamp = 90f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Mouse.current.delta.x.ReadValue() * mouseSensitivity * Time.deltaTime;
        float mouseY = Mouse.current.delta.y.ReadValue() * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera up and body down
        xRotation -= mouseY;
        yRotation += mouseX;

        // Clamp the vertical rotation to prevent looking too far up or down
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);

        // Apply the rotations to the camera and body
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

    }
} 