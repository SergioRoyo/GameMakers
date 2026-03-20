using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Chicken_take : MonoBehaviour
{
    public Transform manos;
    public float fuerzaLanzamiento = 5f;
    public float esperaParaRecoger = 2f;

    private GameObject objetoCerca; // El pollo que detecta el trigger
    public bool take = false;

    private static GameObject ultimoDueno;
    private static float tiempoUltimoDrop;

    public UnityEngine.Color[] Listacolores;
    public int Colores = 0;
    public int Tiempo = 0;

    public NavMeshAgent polloagent;
    public GameObject chickenGoal;
    public GameObject pollo;

    public float timeCaidaPollo = 2f;
    public Chicken_gravity chicken_Gravity;
    public ControladorJugador controladorJugador;

    public float timeToResetCollider = 2f;
    private float timer = 0f;
    public bool aire = false;
    public float polloSpeed = 10;

    public float dropForceUp = 0.55f;
    public float dropForceForward = 0.75f;
    public FLACO_CONTROLLER flaco_controller;

    [Header("Configuración de Destinos")]
    public List<Transform> listaDeDestinos = new List<Transform>(); // Lista donde arrastraremos los objetivos
    private Transform destinoMasCercano;

    //public KeyCode teclaInteractuar = KeyCode.E;

    void Start()
    {
        flaco_controller= GetComponent<FLACO_CONTROLLER>();
        aire = false;

        if (polloagent != null)
            polloagent.destination = chickenGoal.transform.position; // le da destino al navmesh del pollo
        controladorJugador = GetComponent<ControladorJugador>();
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
            //agregamos quien es el pollo por codigo, el destino del naavmesh
            pollo = GameObject.FindGameObjectWithTag("Pollo");
            polloagent = pollo.transform.GetComponent<NavMeshAgent>();
            chicken_Gravity = pollo.GetComponent<Chicken_gravity>();
            chickenGoal = GameObject.FindGameObjectWithTag("TOXIC");
            polloagent.destination = chickenGoal.transform.position;
            polloagent.speed = polloSpeed;
            GameObject[] metas = GameObject.FindGameObjectsWithTag("TOXIC");
            listaDeDestinos.Clear(); // Limpiamos la lista anterior
            foreach (GameObject meta in metas)
            {
                listaDeDestinos.Add(meta.transform);
            }

        }
    }
    Transform ObtenerDestinoMasCercano()
    {
        Transform masCercano = null;
        float distanciaMinima = Mathf.Infinity; // Empezamos con una distancia infinita
        Vector3 posicionActual = pollo.transform.position;

        foreach (Transform destino in listaDeDestinos)
        {
            if (destino != null)
            {
                float distancia = Vector3.Distance(posicionActual, destino.position);
                if (distancia < distanciaMinima)
                {
                    distanciaMinima = distancia;
                    masCercano = destino;
                }
            }
        }
        return masCercano;
    }

    // Función para actualizar el destino del NavMesh
    void ActualizarDestinoIA()
    {
        if (polloagent != null && polloagent.enabled)
        {
            destinoMasCercano = ObtenerDestinoMasCercano();
            if (destinoMasCercano != null)
            {
                polloagent.SetDestination(destinoMasCercano.position);
            }
        }
    }

    void Update()
    {

        if (aire)
        {

            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                Collider col = pollo.GetComponent<Collider>();
                if (col != null) col.enabled = true;
                aire = false;
            }
        }

        if (pollo != null)
        {

            if (chicken_Gravity.sueleando)
            {
                Rigidbody rb = pollo.GetComponent<Rigidbody>();
                rb.isKinematic = true;
                polloagent.enabled = true;
                ActualizarDestinoIA();
                //polloagent.SetDestination(chickenGoal.transform.position);
            }
        }
    }
    public void OnTake()
    {
        if (take)// si tienes el pollo llama a la funcion de lanzar el pollo
        {
            DropChicken();
        }
        else if (!take && objetoCerca != null)// si el objeto cerca es el pollo
        {
            bool esDiferenteJugador = ultimoDueno != gameObject; //se detecta si es el ultimo jugador que cogio el pollo
            bool tiempoCumplido = Time.time > tiempoUltimoDrop + esperaParaRecoger; // se mira si se cumple el tiempo de cooldown

            if (esDiferenteJugador && tiempoCumplido) //si se cumplen, coges el pollo
            {

                StopAllCoroutines();
                TakeChicken();
            }
        }
    }

    void TakeChicken()
    {
        
        chicken_Gravity.sueleando = false;
        take = true;

        polloagent.enabled = false;//  se desactiva el navmeshpara que el pollo no se mueva ni tenga un destino


        pollo.transform.SetParent(manos);
        pollo.transform.localPosition = Vector3.zero;
        pollo.transform.localRotation = Quaternion.identity;

        // 3. F�SICAS
        Rigidbody rb = pollo.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        Collider col = pollo.GetComponent<Collider>();
        if (col != null) col.enabled = false;

        StartCoroutine(changeColor());
    }

    public void DropChicken()
    {
        if (!flaco_controller.scaling)
        {
            controladorJugador.animator.SetBool("runFP", false);
            controladorJugador.animator.SetBool("descansoFP", false);
            controladorJugador.animator.SetTrigger("throwF");

        }
        else if (flaco_controller.scaling)

        {


            controladorJugador.animator.SetBool("runFPS", false);
            controladorJugador.animator.SetBool("descansoFPS", false);

            controladorJugador.animator.SetTrigger("throwFS");

        }

        //if(corriendo)
        //{
        //    controladorJugador.animator.SetTrigger("throwRun");
        //}
        //else
        //{

        //    controladorJugador.animator.SetTrigger("throw");
        //}
        if (!take) return;

        take = false;
        StopAllCoroutines(); // terminamos las corrutinas de cambio nde color y de cuenta atras de soltar al pollo
        pollo.GetComponent<Renderer>().material.color = Listacolores[0];

        ultimoDueno = gameObject; //asignamos que este jugador es el ultimoo dueño del pollo
        tiempoUltimoDrop = Time.time; //asignamos el tiempo

        pollo.transform.SetParent(null); //desemparentamos al pollo para que ya no este en las manos del personaje
        pollo.transform.position = transform.position + transform.forward * 0.7f;

        Rigidbody rb = pollo.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.AddForce((Vector3.up * dropForceUp + transform.forward * dropForceForward) * fuerzaLanzamiento, ForceMode.Impulse);
            aire = true;
        }

        Tiempo = 0;
        Colores = 0;

        timer = timeToResetCollider;
    }


    void ReactivarNavMesh()
    {
        polloagent.enabled = true;
        polloagent.destination = chickenGoal.transform.position;
    }


    private void OnTriggerStay(Collider other)
    {
        if (!take && other.CompareTag("Pollo"))
            objetoCerca = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!take && other.CompareTag("Pollo"))
            objetoCerca = null;
    }


    IEnumerator changeColor()
    {
        yield return new WaitForSeconds(1f);
        if (Tiempo >= 5)
        {
            DropChicken();
            pollo.GetComponent<Renderer>().material.color = Listacolores[0];
        }
        else
        {
            pollo.GetComponent<Renderer>().material.color = Listacolores[Colores % Listacolores.Length];
            Colores++;
            Tiempo++;
            StartCoroutine(changeColor());
        }
    }
}