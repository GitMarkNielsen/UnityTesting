using UnityEngine;
using UnityEngine.InputSystem;

public class Movement :MonoBehaviour
{

    private Vector2 moveInput;
    public float speed = 500f;
    private Rigidbody rb;
    
    
    void Awake()
    {
    rb = GetComponent<Rigidbody>();
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log("Inputs working" +  moveInput);
        
    }
    public void CalculateMovement()
    {
        rb.AddForce(new Vector3(moveInput.x * speed,0,moveInput.y * speed));

    }


}
