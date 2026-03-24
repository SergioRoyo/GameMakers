using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gordo_Controller : MonoBehaviour
{
    public ControladorJugador controladorJugador;
    public Chicken_take chicken_Take;
    public float speedMultiply = 5f;
    public float resetSpeed = 2f;
    public bool canRodar = true;
    public float rodandoTime = 3f;
    public GameObject habilidad2;
    public bool stayHabilidad2 = false;
    public GameObject gordoTraje;
    bool coolDown = false;

    public Slider visualCoolDown;
    public float CoolTimer = 0;
    public float CoolTime = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        controladorJugador = GetComponent<ControladorJugador>();
        chicken_Take = GetComponent<Chicken_take>();
    }

    // Update is called once per frame
    void Update()
    {
        if (coolDown)
        {

            CoolTimer += Time.deltaTime;
            visualCoolDown.value = CoolTimer;
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

    public void OnHabilidad1() //Habilidad de rodar
    {
        if (!enabled) return;
        if (!coolDown)
        {

            if (controladorJugador.isGrounded && Physics.Raycast(transform.position, Vector3.down, controladorJugador.distanciaRayo, controladorJugador.capaSuelo) && canRodar)
            {
                controladorJugador.colliderPersonaje.radius = 0.15f;
                controladorJugador.colliderPersonaje.height = 0.9376385f;
                controladorJugador.colliderPersonaje.center = new Vector3(0, 0.2804004f, 0.05180952f);
                StartCoroutine(Rodar());
            }

        }

    }
    public IEnumerator Rodar()
    {
        canRodar = false;
        controladorJugador.speed = controladorJugador.speed * speedMultiply;
        yield return new WaitForSeconds(rodandoTime);
        controladorJugador.speed = resetSpeed;
        canRodar = true;
        controladorJugador.colliderPersonaje.radius = 0.4f;
        controladorJugador.colliderPersonaje.height = 1.459375f;
        controladorJugador.colliderPersonaje.center = new Vector3(0, 0.5412684f, 0.05180952f);
        CoolTimer = 0;
        visualCoolDown.value = 0;
        StartCoroutine(DashCoolDown());
    }
    public void OnHabilidad2()
    {
        if (!enabled) return;
        if (!chicken_Take.take && stayHabilidad2)
        {

            habilidad2.SetActive(true);
            gordoTraje.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!enabled) return;
        if (other.gameObject.CompareTag("Habilidad2"))
        {
            habilidad2 = other.transform.GetChild(0).gameObject;
            stayHabilidad2 = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!enabled) return;
        if (other.gameObject.CompareTag("Habilidad2"))
        {
            stayHabilidad2 = false;
            other.transform.GetChild(0).gameObject.SetActive(false);
            gordoTraje.SetActive(true);

        }
    }
    IEnumerator DashCoolDown()
    {
        coolDown = true;
        yield return new WaitForSeconds(CoolTime);
        coolDown = false;
    }
}
