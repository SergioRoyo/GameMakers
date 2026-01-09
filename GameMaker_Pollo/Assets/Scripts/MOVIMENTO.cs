using UnityEngine;
using UnityEngine.InputSystem;

public class MOVIMENTO : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;

    private Vector2 input;
    private Rigidbody rb;
    private bool isGrounded;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputValue value)
    {
        input = value.Get<Vector2>();
    }
    public void OnJump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    void FixedUpdate()
    {
        // 1. Obtenemos la dirección de la cámara ignorando la altura (Y)
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        // 2. Calculamos el movimiento relativo a la cámara
        Vector3 movement = (forward * input.y + right * input.x);
        //Vector3 movement = new Vector3(input.x, 0f, input.y);
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    private void OnCollisionStay(Collision col) => isGrounded = true;
    private void OnCollisionExit(Collision col) => isGrounded = false;
}
