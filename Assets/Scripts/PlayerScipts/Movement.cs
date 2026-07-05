using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class Movement : MonoBehaviour
{
    // inputs
    private Vector2 moveInput;
    private Vector2 mouseInput;
    
    public float speed = 500f;
    private Rigidbody rb;
    private Camera cam;

    //Grounded Detection
    [SerializeField] private LayerMask groundLayer;
    private float groundDetectRange = 0.1f;
    [SerializeField] private BoxCollider groundCheckCollider;
    [SerializeField] private Vector3 currentDown;



    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
        groundCheckCollider = GetComponent<BoxCollider>();
        currentDown = Vector3.down;
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
                
    }
    public void CalculateMovement()
    {
        
        //getting camera orientation
        Vector3 camForward = cam.transform.forward;
        Vector3 camRight = cam.transform.right;
        camForward.y = 0f;
        camRight.y = 0f;

        // Calculate the final movement direction based on input
        // W/S (moveInput.y) moves along the camera's forward axis
        // A/D (moveInput.x) moves along the camera's right axis
        Vector3 moveDirection = (camForward * moveInput.y) + (camRight * moveInput.x);

       
        if (IsGrounded())
        {
        rb.AddForce(moveDirection * speed, ForceMode.Force);
        }
    }

    private bool IsGrounded()
    {
        Vector3 center = groundCheckCollider.bounds.center;
         Vector3 size = groundCheckCollider.bounds.size;

        return Physics.BoxCast(center, size / 2f, currentDown, rb.transform.rotation, groundDetectRange );
    }


}
