using UnityEngine;

public class Chicken_gravity : MonoBehaviour
{
    public float gravity = -5f;
    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb != null)
        {
           
            float verticalSpeed = rb.linearVelocity.y; 

            if (verticalSpeed > 0.1f)
            {
                gravity = -10f;
            }
            else if (verticalSpeed < -0.1f)
            {
                gravity = -2.5f;
            }
        }
    }
    void FixedUpdate()
    {
        
        Vector3 gravedadCustom = transform.up * gravity;

       
        rb.AddForce(gravedadCustom, ForceMode.Acceleration);
    }

}
