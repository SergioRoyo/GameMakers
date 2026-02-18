using UnityEngine;

public class FLACO_CONTROLLER : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Ability Settings")]
    public string abilityButton = "Habilidad1"; // Nombre del botón en Input Manager

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleMovement();
        HandleAbility();
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    void HandleAbility()
    {
        if (Input.GetButtonDown(abilityButton))
        {
            CheckNearbyRampas();
        }
    }

    void CheckNearbyRampas()
    {
        // Buscar rampas cercanas
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3f);

        foreach (Collider hit in hitColliders)
        {
            RampaPrefab rampa = hit.GetComponent<RampaPrefab>();
            if (rampa != null && !rampa.GetIsActive())
            {
                rampa.ActivarRampa();
                break; // Activar solo la primera rampa cercana
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
