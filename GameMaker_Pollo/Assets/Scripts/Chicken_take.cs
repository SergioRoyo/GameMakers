using System.Drawing;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class Chicken_take : MonoBehaviour
{
    public Transform manos;
    public float fuerzaLanzamiento = 5f;
    public float esperaParaRecoger = 2f; // Segundos de espera

    private GameObject objetoCerca;
    private bool estaCargando = false;

    // Variables estáticas (compartidas por todos los jugadores)
    private static GameObject ultimoDueno;
    private static float tiempoUltimoDrop;

    public UnityEngine.Color[] Listacolores;
    public int Colores = 0;
    public int Tiempo = 0;
    public NavMeshAgent polloagent;
    public GameObject chickenGoal;

    public GameObject pollo;

    public void Start()
    {
        polloagent.destination = chickenGoal.transform.position;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (estaCargando)
            {
                polloagent.isStopped = false;
                polloagent.destination = chickenGoal.transform.position;
                DropChicken();
            }
            else if (objetoCerca != null)
            {
                
                bool esDiferenteJugador = ultimoDueno != gameObject;
                bool tiempoCumplido = Time.time > tiempoUltimoDrop + esperaParaRecoger;

                if (esDiferenteJugador && tiempoCumplido)
                {
                    polloagent.isStopped = true;
                    TakeChicken();
                }
                
            }
        }
    }

    void TakeChicken()
    {
        
        estaCargando = true;
        objetoCerca.transform.SetParent(manos);
        objetoCerca.transform.localPosition = Vector3.zero;
        objetoCerca.transform.localRotation = Quaternion.identity;

        if (objetoCerca.GetComponent<Rigidbody>())
            objetoCerca.GetComponent<Rigidbody>().isKinematic = true;

        if (objetoCerca.GetComponent<Collider>())
            objetoCerca.GetComponent<Collider>().enabled = false;
        StartCoroutine(changeColor());
    }

    public void DropChicken()
    {
        estaCargando = false;

        // Registramos quién lo soltó y en qué segundo
        ultimoDueno = gameObject;
        tiempoUltimoDrop = Time.time;

        Rigidbody rb = objetoCerca.GetComponent<Rigidbody>();
        Collider col = objetoCerca.GetComponent<Collider>();

        objetoCerca.transform.SetParent(null);

        // Separar del cuerpo para no empujar al jugador
        objetoCerca.transform.position = transform.position + transform.forward * 1.2f;

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.AddForce((Vector3.up + transform.forward * 0.3f) * fuerzaLanzamiento, ForceMode.Impulse);
        }

        if (col != null)
            col.enabled = true;

        objetoCerca = null;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!estaCargando && other.CompareTag("Pollo"))
        {
            objetoCerca = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!estaCargando && other.CompareTag("Pollo"))
        {
            objetoCerca = null;
        }
    }
    IEnumerator changeColor()
    {
        yield return new WaitForSeconds(1f);
        if (Tiempo == 5)
        {
           DropChicken();
            pollo.GetComponent<Renderer>().material.color = Listacolores[0];

        }
        else
        {
            pollo.GetComponent<Renderer>().material.color = Listacolores[Colores++];
            Tiempo++;

            StartCoroutine(changeColor());
        }



    }
}