using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FLACO_CONTROLLER : MonoBehaviour
{
    [SerializeField] public GameObject rampaVisual;
    [SerializeField] public GameObject rampaGhost;
    public bool rampaSwitch;
    public ControladorJugador controladorJugador;
    public float H2gordoForce = 20;
    public PhysicsMaterial noFriction;
    public PhysicsMaterial fullFriction;
    public GameObject flacoTraje;
    public GameObject flacoTrajeScaled;
    CapsuleCollider col;
    public float scaleTime = 3f;
    public bool scaling = false;
    public Vector3 scaleY = new Vector3(1, 2, 1);
    public bool sCoolDown = false;

    public Slider visualCoolDown;
    public float CoolTimer = 0;
    public float CoolTime = 3;

    public float rampaJump = 6f;
    public bool habilidad2=true;
    void Start()
    {
        habilidad2 = false;
        rampaSwitch = false;
        controladorJugador = this.gameObject.GetComponent<ControladorJugador>();
        col = GetComponent<CapsuleCollider>();
    }
    private void Update()
    {
        if (sCoolDown)
        {

            CoolTimer += Time.deltaTime;
            visualCoolDown.value = CoolTimer;
        }
        if (habilidad2)
        {
            controladorJugador.jumpForce = rampaJump;
        }
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += BusquedaDeObjetos; //evento
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= BusquedaDeObjetos;
    }
    void BusquedaDeObjetos(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GAMEPLAY_Scene")
        {
           
            if (this.gameObject == GameObject.Find("Jugador_1"))
            {
               
                visualCoolDown = Canvas_Manager.Instance.P1slider;
            }
            else if (this.gameObject == GameObject.Find("Jugador_2"))
            {
               
                visualCoolDown = Canvas_Manager.Instance.P2slider;
            }
           
            visualCoolDown.maxValue = CoolTime;
            visualCoolDown.minValue = 0;
            visualCoolDown.value = 0;
        }
    }


    void OnTriggerEnter(Collider other)
    {

        if (!enabled) return;

        if (other.CompareTag("Rampa"))
        {
            col.material = fullFriction;
            rampaGhost = other.transform.GetChild(1).gameObject;
            rampaGhost.SetActive(true);
            rampaSwitch = true;
            rampaVisual = other.transform.GetChild(0).gameObject;
          

           
            
        }
        if (other.CompareTag("H2Gordo"))
        {
            controladorJugador.rb.AddForce(Vector3.up * H2gordoForce, ForceMode.Impulse);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!enabled) return;

        if (other.CompareTag("Rampa"))
        {
            rampaGhost.SetActive(false);
            col.material = noFriction;
            flacoTraje.SetActive(true);
            other.transform.GetChild(0).gameObject.SetActive(false);
            rampaSwitch = false;
            controladorJugador.jumpForce = controladorJugador.jumpForceFlaco;
            habilidad2 = false;
        }

    }
    private void OnHabilidad2()
    {
        if (!enabled) return;

        if (rampaSwitch)
        {
           habilidad2 = true;
            rampaVisual.SetActive(true);
            rampaGhost.SetActive(false);
            flacoTraje.SetActive(false);
        }
    }
    private void OnHabilidad1()
    {
        if (!enabled) return;
        if (!sCoolDown)
        {

            if (!scaling)
            {
                StartCoroutine(Scale());
            }
        }
    }

    IEnumerator Scale()
    {
        scaling = true;
        flacoTraje.SetActive(false);
        flacoTrajeScaled.SetActive(true);

        controladorJugador.colliderPersonaje.radius = 0.2028357f;
        controladorJugador.colliderPersonaje.height = 2.522687f;
        controladorJugador.colliderPersonaje.center = new Vector3(0, 1.072925f, 0.05180952f);

        controladorJugador.manosFlaco.transform.localPosition = new Vector3 (0.106f, 1.885f, 0.656f);

        yield return new WaitForSeconds(scaleTime);

        controladorJugador.colliderPersonaje.radius = 0.2028357f;
        controladorJugador.colliderPersonaje.height = 1.900662f;
        controladorJugador.colliderPersonaje.center = new Vector3(0, 0.7619121f, 0);

        controladorJugador.manosFlaco.transform.localPosition = new Vector3(0.106f, 1.391f, 0.656f);

        flacoTraje.SetActive(true);
        flacoTrajeScaled.SetActive(false);
        scaling = false;
                CoolTimer = 0;
                visualCoolDown.value = 0;
            StartCoroutine(ScaleCoolDown());
    }
    IEnumerator ScaleCoolDown()
    {
        sCoolDown = true;
        yield return new WaitForSeconds(3);
        sCoolDown = false;
    }
}
