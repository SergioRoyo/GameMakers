using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Chicken_take : MonoBehaviour
{
    public Transform manos;
    public float fuerzaLanzamiento = 5f;
    public float esperaParaRecoger = 2f;

    private GameObject objetoCerca; // El pollo que detecta el trigger
    private bool estaCargando = false;

    private static GameObject ultimoDueno;
    private static float tiempoUltimoDrop;

    public UnityEngine.Color[] Listacolores;
    public int Colores = 0;
    public int Tiempo = 0;

    public NavMeshAgent polloagent;
    public GameObject chickenGoal;
    public GameObject pollo;
    public KeyCode teclaInteractuar = KeyCode.E;

    void Start()
    {
        if (polloagent != null)
            polloagent.destination = chickenGoal.transform.position;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += BusquedaDeObjetos;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= BusquedaDeObjetos;
    }
    void BusquedaDeObjetos(Scene scene, LoadSceneMode mode)
    {
        if (scene.name== "GAMEPLAY_Scene")
        {
            //pollo= GameObject.Find ("Pollo");
            pollo= GameObject.FindGameObjectWithTag("Pollo");
            polloagent= pollo.transform.GetComponent<NavMeshAgent>();
            chickenGoal = GameObject.FindGameObjectWithTag("TOXIC");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(teclaInteractuar))
        {
            if (estaCargando)
            {
                DropChicken();
            }
            else if (objetoCerca != null)
            {
                bool esDiferenteJugador = ultimoDueno != gameObject;
                bool tiempoCumplido = Time.time > tiempoUltimoDrop + esperaParaRecoger;

                if (esDiferenteJugador && tiempoCumplido)
                {
                    TakeChicken();
                }
            }
        }
    }

    void TakeChicken()
    {
        estaCargando = true;

        polloagent.enabled = false;

       
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
        StopAllCoroutines();

        ultimoDueno = gameObject;
        tiempoUltimoDrop = Time.time;

        pollo.transform.SetParent(null);
        pollo.transform.position = transform.position + transform.forward * 1.5f;

        Rigidbody rb = pollo.GetComponent<Rigidbody>();
        Collider col = pollo.GetComponent<Collider>();

        if (col != null) col.enabled = true;

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.AddForce((Vector3.up + transform.forward * 0.5f) * fuerzaLanzamiento, ForceMode.Impulse);
        }

        StartCoroutine(EsperarAterrizaje());

        Tiempo = 0;
        Colores = 0;
    }
    IEnumerator EsperarAterrizaje()
    {
        
        yield return new WaitForSeconds(.8f);

     
        Rigidbody rb = pollo.GetComponent<Rigidbody>();
        while (rb != null && Mathf.Abs(rb.linearVelocity.y) > 0.05f)
        {
            yield return null;
        }

        if (pollo != null)
        {
            
            rb.isKinematic = true;

          
            polloagent.enabled = true;

            
            polloagent.Warp(pollo.transform.position);

         
            polloagent.SetDestination(chickenGoal.transform.position);
        }
    }

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