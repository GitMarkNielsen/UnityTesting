using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [Header("Sensitivity Settings")]
    [SerializeField] private float mouseSensitivity = 15f;

    [Header("References")]
    [SerializeField] private Transform playerBody; // Drag your Player GameObject here

    private Vector2 lookInput;
    private float xRotation = 0f; // Stores vertical rotation to clamp it

    void Start()
    {
        // Locks the mouse cursor to the center of the screen and hides it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Hook this up to your new "Look" Action in the Player Input component
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    // Camera rotation is visual feedback, so it belongs in Update() for maximum smoothness
    void Update()
    {
        // 1. Calculate look values based on sensitivity and framerate
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        // 2. Handle Vertical Look (Pitch)
        // Moving mouse UP looks up (- equals up in Unity's internal rotation logic)
        xRotation -= mouseY;

        // Clamp the vertical look between -90 and 90 degrees so you can't do backflips
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply vertical rotation locally to just the camera
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // 3. Handle Horizontal Look (Yaw)
        // Rotate the entire player body left and right so the character turns with the camera
        if (playerBody != null)
        {
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}