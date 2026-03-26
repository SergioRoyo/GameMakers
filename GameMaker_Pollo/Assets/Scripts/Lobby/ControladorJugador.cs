using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class ControladorJugador : MonoBehaviour
{
    public GameObject modeloGordo;
    public GameObject modeloFlaco;
    public Transform manosGordo;
    public Transform manosFlaco;

    public float speed = 5f;

    public float jumpForce = 5f;

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
    public float distanciaRayo =1.1f;
    public LayerMask capaSuelo;
    public Chicken_take chicken_Take;
    public FLACO_CONTROLLER flaco_controller;
    public CapsuleCollider colliderPersonaje;

    public float gravity = -9.8f;
    public float downGravity = -18f;
    //public GameObject rigGordo;

    public Animator animator;
    public Animator gordoAnimator;
    public Animator flacoAnimator;
    public int caratulas = 1;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Esto hace que el jugador no se borre al cambiar de escena
        DontDestroyOnLoad(this.gameObject);

    }
    private void Start()
    {
        flaco_controller = GetComponent<FLACO_CONTROLLER>(); //deteccion de scripts
        chicken_Take = GetComponent<Chicken_take>();
    }
    private void Update()
    {
        if(caratulas == 1)
        {
            if (Canvas_Manager.Instance != null)
            {

            Canvas_Manager.Instance.Cartulas1();
            }
        }
        else if(caratulas == 2)
        {
            if (Canvas_Manager.Instance != null)
            {
                Canvas_Manager.Instance.Cartulas2();
            }
        }


        if (rb != null)
        {

            float verticalSpeed = rb.linearVelocity.y;

            if (verticalSpeed > 0.1f)//sistema de gravedad del pollo simulacion de planear
            {
                gravity = -9.8f;
            }
            else if (verticalSpeed < -0.1f)
            {
                gravity = -9.8f;
            }
        }
        if (animator != null)
        {


            if (animator.gameObject.activeInHierarchy)//animaciones cuando se mueve o para
            {

                if (input.magnitude > 0.1f)
                {
                    if (animator == gordoAnimator)
                    {
                        if (!chicken_Take.take)
                        {
                            animator.SetBool("runGP", false);

                            animator.SetBool("descanso", false);
                            animator.SetBool("descansoGP", false);
                            animator.SetBool("runG", true);
                        }
                        else if (chicken_Take.take)
                        {
                            animator.SetBool("runG", false);
                            animator.SetBool("descanso", false);
                            animator.SetBool("descansoGP", false);
                            animator.SetBool("runGP", true);
                        }

                    }
                    else if (animator == flacoAnimator)
                    {


                        if (chicken_Take.take && !flaco_controller.scaling)
                        {
                            animator.SetBool("runF", false);
                            animator.SetBool("runFS", false);
                            animator.SetBool("runFPS", false);


                            animator.SetBool("runFP", true);
                        }
                        else if (chicken_Take.take && flaco_controller.scaling)
                        {

                            animator.SetBool("runF", false);
                            animator.SetBool("runFP", false);
                            animator.SetBool("runFS", false);


                            animator.SetBool("runFPS", true);
                        }
                        else if (!chicken_Take.take && flaco_controller.scaling)
                        {
                            animator.SetBool("runF", false);
                            animator.SetBool("runFP", false);
                            animator.SetBool("runFPS", false);

                            animator.SetBool("runFS", true);
                        }
                        else if (!chicken_Take.take && !flaco_controller.scaling)
                        {

                            animator.SetBool("runFP", false);
                            animator.SetBool("runFS", false);
                            animator.SetBool("runFPS", false);



                            animator.SetBool("runF", true);


                        }
                    }
                }
                else
                {
                    if (animator == gordoAnimator)
                    {

                        if (!chicken_Take.take)
                        {
                            animator.SetBool("runGP", false);

                            animator.SetBool("descansoGP", false);
                            animator.SetBool("runG", false);
                            animator.SetBool("descanso", true);
                        }
                        else if (chicken_Take.take)
                        {
                            animator.SetBool("runG", false);
                            animator.SetBool("descanso", false);
                            animator.SetBool("runGP", false);
                            animator.SetBool("descansoGP", true);
                        }



                    }
                    else if (animator == flacoAnimator)
                    {
                        if (!chicken_Take.take)
                        {


                            if (flaco_controller.scaling)
                            {
                                animator.SetBool("runFS", false);
                                animator.SetBool("descansoFPS", false);
                                animator.SetBool("descansoFP", false);
                                animator.SetBool("descansoFS", true);
                            }
                            else
                            {
                                animator.SetBool("runF", false);
                                animator.SetBool("descansoFPS", false);
                                animator.SetBool("descansoFP", false);
                                animator.SetBool("descansoFS", false);
                                animator.SetTrigger("descanso");

                            }
                        }
                        else if (chicken_Take.take)
                        {
                            if (flaco_controller.scaling)
                            {
                                animator.SetBool("runFPS", false);

                                animator.SetBool("descansoFP", false);
                                animator.SetBool("descansoFS", false);

                                animator.SetBool("descansoFPS", true);
                            }
                            else
                            {
                                animator.SetBool("descansoFPS", false);

                                animator.SetBool("descansoFS", false);
                                animator.SetBool("runFP", false);
                                animator.SetBool("descansoFP", true);
                            }
                        }
                    }

                }
            }
        }
    }

   
    //  LOBBY

    public void PonerseTrajeGordo()// sistema en el cual se le dan las propiedades al gordo
    {
        GetComponent<Gordo_Controller>().enabled = true;
        GetComponent<FLACO_CONTROLLER>().enabled = false;

        chicken_Take.manos = manosGordo;
        speed = speedGordo;
        jumpForce = jumpForceGordo;
        chicken_Take.dropForceForward = dropForceForwardGordo;
        chicken_Take.dropForceUp = dropForceUpGordo;

        colliderPersonaje.radius = 0.4f;
        colliderPersonaje.height = 1.459375f;
        colliderPersonaje.center = new Vector3(0, 0.5412684f, 0.05180952f);

        if (modeloGordo) modeloGordo.SetActive(true);
        if (modeloFlaco) modeloFlaco.SetActive(false);
        animator = gordoAnimator;
    }

    public void PonerseTrajeFlaco()// sistema en el cual se le dan las propiedades al flaco
    {
        GetComponent<Gordo_Controller>().enabled = false;
        GetComponent<FLACO_CONTROLLER>().enabled = true;

        speed = speedFlaco;
        jumpForce = jumpForceFlaco;


        chicken_Take.dropForceUp = dropForceUpFlaco;
        chicken_Take.dropForceForward = dropForceForwardFlaco;
        chicken_Take.manos = manosFlaco;


        colliderPersonaje.radius = 0.2028357f;
        colliderPersonaje.height = 1.900662f;
        colliderPersonaje.center = new Vector3(0, 0.7619121f, 0);

        if (modeloGordo) modeloGordo.SetActive(false);
        if (modeloFlaco) modeloFlaco.SetActive(true);
        animator = flacoAnimator;
    }

    // SISTEMA DE INPUT (MANDOS) 

    // Esta función se llama sola cuando mueves el stick
    public void OnMove(InputValue value)//moverse
    {
        input = value.Get<Vector2>();

    }

    // Esta función se llama sola cuando pulsas el botón Sur (A/X)
    public void OnJump()
    {
        if (isGrounded && Physics.Raycast(transform.position, Vector3.down, distanciaRayo, capaSuelo))//salta solo si esta en el suelo y el raycast llega al suelo 
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        }

    }

    // --- FÍSICAS Y MOVIMIENTO ---

    void FixedUpdate()
    {
        Vector3 gravedadCustom = transform.up * gravity;


        rb.AddForce(gravedadCustom, ForceMode.Acceleration);
        // 1. Obtenemos la dirección de la cámara ignorando la altura (Y)

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        // 2. Calculamos el movimiento  hacia dónde mira la cámara
        Vector3 movement = (forward * input.y + right * input.x);

        // 3. Movemos el Rigidbody
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        // 4. Rotación del personaje (para que mire hacia donde camina)
        if (movement != Vector3.zero)
        {
            Quaternion rotacionObjetivo = Quaternion.LookRotation(movement);

            // giro  suave y no instantáneo
            Quaternion rotacionSuave = Quaternion.RotateTowards(rb.rotation, rotacionObjetivo, velocidadGiro * Time.fixedDeltaTime);

            rb.MoveRotation(rotacionSuave);
        }
    }

    // --- DETECCIÓN DE SUELO ---

    private void OnCollisionStay(Collision col)
    {
        isGrounded = true;
        gravity = -9.8f;
    }
    private void OnCollisionExit(Collision col) => isGrounded = false;
}