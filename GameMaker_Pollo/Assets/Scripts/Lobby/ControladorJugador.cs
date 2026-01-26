using UnityEngine;
using UnityEngine.InputSystem;


public class ControladorJugador : MonoBehaviour
{
    public GameObject modeloGordo;
    public GameObject modeloFlaco;

    public float speed = 5f;
    public float jumpForce = 5f;
    public float velocidadGiro = 720f;

    private Rigidbody rb;
    private Vector2 input; 
    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Esto hace que el jugador no se borre al cambiar de escena
        DontDestroyOnLoad(this.gameObject);
    }

    //  LOBBY

    public void PonerseTrajeGordo()
    {
        if (modeloGordo) modeloGordo.SetActive(true);
        if (modeloFlaco) modeloFlaco.SetActive(false);
    }

    public void PonerseTrajeFlaco()
    {
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
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // --- FÍSICAS Y MOVIMIENTO ---

    void FixedUpdate()
    {

        // 1. Obtenemos la dirección de la cámara ignorando la altura (Y)
        // Esto es vital para que si la cámara mira hacia abajo, el personaje no intente clavarse en el suelo
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