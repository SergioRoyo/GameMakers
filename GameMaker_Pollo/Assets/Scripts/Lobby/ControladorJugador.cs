using UnityEngine;
using UnityEngine.InputSystem;


public class ControladorJugador : MonoBehaviour
{
    public GameObject modeloGordo;
    public GameObject modeloFlaco;
    public Transform manosGordo;
    public Transform manosFlaco;

    public float speed = 5f;

    float jumpForce = 5f;

    public float speedGordo = 4f;
    public float jumpForceGordo = 5f;
    public float dropForceUpGordo = .7f;
    public float dropForceForwardGordo = .8f;

    public float speedFlaco = 6f;
    public float jumpForceFlaco = 7f;
    public float dropForceUpFlaco = .5f;
    public float dropForceForwardFlaco = .5f;

    public float velocidadGiro = 720f;

    public Rigidbody rb;
    private Vector2 input;
    public bool isGrounded;
    public float distanciaRayo = 1.1f;
    public LayerMask capaSuelo;
    public Chicken_take chicken_Take;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Esto hace que el jugador no se borre al cambiar de escena
        DontDestroyOnLoad(this.gameObject);

    }
    private void Start()
    {
        chicken_Take = GetComponent<Chicken_take>();
    }
    private void Update()
    {
        Debug.DrawRay(transform.position, Vector3.down * distanciaRayo, Color.red);
    }

    //  LOBBY

    public void PonerseTrajeGordo()
    {
        GetComponent<Gordo_Controller>().enabled = true;
        GetComponent<FLACO_CONTROLLER>().enabled = false;

        chicken_Take.manos = manosGordo;
        speed = speedGordo;
        jumpForce = jumpForceGordo;
        chicken_Take.dropForceForward = dropForceForwardGordo;
        chicken_Take.dropForceUp = dropForceUpGordo;
        if (modeloGordo) modeloGordo.SetActive(true);
        if (modeloFlaco) modeloFlaco.SetActive(false);
    }

    public void PonerseTrajeFlaco()
    {
        GetComponent<Gordo_Controller>().enabled = false;
        GetComponent<FLACO_CONTROLLER>().enabled = true;

        chicken_Take.manos = manosFlaco;
        speed = speedFlaco;
        jumpForce = jumpForceFlaco;
        chicken_Take.dropForceUp = dropForceUpFlaco;
        chicken_Take.dropForceForward = dropForceForwardFlaco;
        if (modeloGordo) modeloGordo.SetActive(false);
        if (modeloFlaco) modeloFlaco.SetActive(true);
    }

    // --- SISTEMA DE INPUT (MANDOS) ---

    // Esta función se llama sola cuando mueves el stick
    public void OnMove(InputValue value)
    {
        input = value.Get<Vector2>();
    }

    // Esta función se llama sola cuando pulsas el botón Sur (A/X)
    public void OnJump()
    {
        if (isGrounded && Physics.Raycast(transform.position, Vector3.down, distanciaRayo, capaSuelo))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

    }

    // --- FÍSICAS Y MOVIMIENTO ---

    void FixedUpdate()
    {

        // 1. Obtenemos la dirección de la cámara ignorando la altura (Y)

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        // 2. Calculamos el movimiento basándonos en hacia dónde mira la cámara
        Vector3 movement = (forward * input.y + right * input.x);

        // 3. Movemos el Rigidbody
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        // 4. Rotación del personaje (para que mire hacia donde camina)
        if (movement != Vector3.zero)
        {
            Quaternion rotacionObjetivo = Quaternion.LookRotation(movement);

            // Quaternion.RotateTowards hace que el giro sea suave y no instantáneo
            Quaternion rotacionSuave = Quaternion.RotateTowards(rb.rotation, rotacionObjetivo, velocidadGiro * Time.fixedDeltaTime);

            rb.MoveRotation(rotacionSuave);
        }
    }

    // --- DETECCIÓN DE SUELO ---

    private void OnCollisionStay(Collision col) => isGrounded = true;
    private void OnCollisionExit(Collision col) => isGrounded = false;
}