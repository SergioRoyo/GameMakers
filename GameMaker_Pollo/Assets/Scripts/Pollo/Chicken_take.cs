using System.Collections;
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
    public bool estaCargando = false;

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

    public float timeToResetCollider = 2f;
    private float timer = 0f;
    public bool aire=false;
    //public KeyCode teclaInteractuar = KeyCode.E;

    void Start()
    {
        aire = false;
        if (polloagent != null)
            polloagent.destination = chickenGoal.transform.position; // le da destino al navmesh del pollo
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
        }
    }

    void Update()
    {
       
        if (aire)
        {

            timer -= Time.deltaTime;
            if(timer <= 0f)
            {
                Collider col = pollo.GetComponent<Collider>();
                if (col != null) col.enabled = true;
                aire = false;
            }
        }
       

        if (chicken_Gravity.sueleando)
        {
            Rigidbody rb = pollo.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            polloagent.enabled = true;
            polloagent.SetDestination(chickenGoal.transform.position);
        }
    }
    public void OnTake()
    {
        if (estaCargando)// si tienes el pollo llama a la funcion de lanzar el pollo
        {
            DropChicken();
        }
        else if (objetoCerca != null)// si el objeto cerca es el pollo
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
        estaCargando = true;

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
        if (!estaCargando) return;

        estaCargando = false;
        StopAllCoroutines(); // terminamos las corrutinas de cambio nde color y de cuenta atras de soltar al pollo
        pollo.GetComponent<Renderer>().material.color = Listacolores[0];

        ultimoDueno = gameObject; //asignamos que este jugador es el ultimoo dueño del pollo
        tiempoUltimoDrop = Time.time; //asignamos el tiempo

        pollo.transform.SetParent(null); //desemparentamos al pollo para que ya no este en las manos del personaje
        pollo.transform.position = transform.position + transform.forward * 0.5f;

        Rigidbody rb = pollo.GetComponent<Rigidbody>();
            
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.AddForce((Vector3.up * .7f + transform.forward * 0.05f) * fuerzaLanzamiento, ForceMode.Impulse);
            aire = true;
        }

        //StartCoroutine(EsperarAterrizaje());

        Tiempo = 0;
        Colores = 0;

        timer = timeToResetCollider;
    }
    //IEnumerator EsperarAterrizaje()
    //{

    //    yield return new WaitForSeconds(timeCaidaPollo);


    //    Rigidbody rb = pollo.GetComponent<Rigidbody>();
    //    while (rb != null && Mathf.Abs(rb.linearVelocity.y) > 0.05f)
    //    {
    //        yield return null;
    //    }

    //    if (pollo != null && !estaCargando)
    //    {

    //        rb.isKinematic = true;


    //        polloagent.enabled = true;


    //        //polloagent.Warp(pollo.transform.position);


    //        polloagent.SetDestination(chickenGoal.transform.position);
    //    }
    //}

    void ReactivarNavMesh()
    {
        polloagent.enabled = true;
        polloagent.destination = chickenGoal.transform.position;
    }


    private void OnTriggerStay(Collider other)
    {
        if (!estaCargando && other.CompareTag("Pollo"))
            objetoCerca = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!estaCargando && other.CompareTag("Pollo"))
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