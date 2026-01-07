using UnityEngine;
using UnityEngine.InputSystem;

public class MOVIMENTO : MonoBehaviour
{
    public float speed = 5f;

    private Vector2 input;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputValue value)
    {
        input = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(input.x, 0f, input.y);
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
