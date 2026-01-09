//using System.Drawing;
//using UnityEngine;
//using System.Collections;
//using UnityEngine.SceneManagement;
//using UnityEngine.AI;

//public class Chicken_take : MonoBehaviour
//{
//    public Transform manos;
//    public float fuerzaLanzamiento = 5f;
//    public float esperaParaRecoger = 2f; // Segundos de espera

//    private GameObject objetoCerca;
//    private bool estaCargando = false;

//    // Variables est�ticas (compartidas por todos los jugadores)
//    private static GameObject ultimoDueno;
//    private static float tiempoUltimoDrop;

//    public UnityEngine.Color[] Listacolores;
//    public int Colores = 0;
//    public int Tiempo = 0;
//    public NavMeshAgent polloagent;
//    public GameObject chickenGoal;

//    public GameObject pollo;

//    public void Start()
//    {
//        polloagent.destination = chickenGoal.transform.position;
//    }
//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.E))
//        {
//            if (estaCargando)
//            {
//                DropChicken();
//                polloagent.isStopped = false;
//                polloagent.destination = chickenGoal.transform.position;
//            }
//            else if (objetoCerca != null)
//            {

//                bool esDiferenteJugador = ultimoDueno != gameObject;
//                bool tiempoCumplido = Time.time > tiempoUltimoDrop + esperaParaRecoger;

//                if (esDiferenteJugador && tiempoCumplido)
//                {
//                    TakeChicken();
//                    polloagent.isStopped = true;
//                }

//            }
//        }
//    }

//    void TakeChicken()
//    {

//        estaCargando = true;
//        objetoCerca.transform.SetParent(manos);
//        objetoCerca.transform.localPosition = Vector3.zero;
//        objetoCerca.transform.localRotation = Quaternion.identity;

//        if (objetoCerca.GetComponent<Rigidbody>())
//            objetoCerca.GetComponent<Rigidbody>().isKinematic = true;

//        if (objetoCerca.GetComponent<Collider>())
//            objetoCerca.GetComponent<Collider>().enabled = false;
//        StartCoroutine(changeColor());
//    }

//    public void DropChicken()
//    {
//        estaCargando = false;

//        // Registramos qui�n lo solt� y en qu� segundo
//        ultimoDueno = gameObject;
//        tiempoUltimoDrop = Time.time;

//        Rigidbody rb = objetoCerca.GetComponent<Rigidbody>();
//        Collider col = objetoCerca.GetComponent<Collider>();

//        objetoCerca.transform.SetParent(null);

//        // Separar del cuerpo para no empujar al jugador
//        objetoCerca.transform.position = transform.position + transform.forward * 1.2f;

//        if (rb != null)
//        {
//            rb.isKinematic = false;
//            rb.AddForce((Vector3.up + transform.forward * 0.3f) * fuerzaLanzamiento, ForceMode.Impulse);
//        }

//        if (col != null)
//            col.enabled = true;

//        objetoCerca = null;
//    }

//    private void OnTriggerStay(Collider other)
//    {
//        if (!estaCargando && other.CompareTag("Pollo"))
//        {
//            objetoCerca = other.gameObject;
//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        if (!estaCargando && other.CompareTag("Pollo"))
//        {
//            objetoCerca = null;
//        }
//    }
//    IEnumerator changeColor()
//    {
//        yield return new WaitForSeconds(1f);
//        if (Tiempo == 5)
//        {
//           DropChicken();
//            pollo.GetComponent<Renderer>().material.color = Listacolores[0];

//        }
//        else
//        {
//            pollo.GetComponent<Renderer>().material.color = Listacolores[Colores++];
//            Tiempo++;

//            StartCoroutine(changeColor());
//        }



//    }
//}

using UnityEngine;
using System.Collections;
using UnityEngine.AI;

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
    public KeyCode teclaInteractuar = KeyCode.E;// El objeto f�sico del pollo

    void Start()
    {
        if (polloagent != null)
            polloagent.destination = chickenGoal.transform.position;
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

        // 1. APAGAR NAVMESH TOTALMENTE (Si no, no se mover� a la mano)
        polloagent.enabled = false;

        // 2. HIJO Y POSICI�N
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

        // 1. SOLTAR
        pollo.transform.SetParent(null);
        pollo.transform.position = transform.position + transform.forward * 1.5f;

        // 2. ACTIVAR F�SICAS (Sin NavMesh a�n)
        Rigidbody rb = pollo.GetComponent<Rigidbody>();
        Collider col = pollo.GetComponent<Collider>();

        if (col != null) col.enabled = true;

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.AddForce((Vector3.up + transform.forward * 0.5f) * fuerzaLanzamiento, ForceMode.Impulse);
        }

        // 3. EN LUGAR DE INVOKE, usamos una comprobaci�n constante
        StartCoroutine(EsperarAterrizaje());

        Tiempo = 0;
        Colores = 0;
    }
    IEnumerator EsperarAterrizaje()
    {
        // 1. Esperar a que el pollo deje de subir y empiece a caer
        yield return new WaitForSeconds(.8f);

        // 2. Esperar hasta que est� cerca del suelo o deje de moverse r�pido
        Rigidbody rb = pollo.GetComponent<Rigidbody>();
        while (rb != null && Mathf.Abs(rb.linearVelocity.y) > 0.05f)
        {
            yield return null;
        }

        // 3. RECONEXI�N CON EL NAVMESH
        if (pollo != null)
        {
            // Importante: Ponemos el Rigidbody en Kinematic para que no interfiera
            rb.isKinematic = true;

            // Activamos el componente
            polloagent.enabled = true;

            // EL TRUCO: Warp obliga al agente a posicionarse correctamente en la malla azul
            // Esto evita que se quede "congelado" o "teletransportado"
            polloagent.Warp(pollo.transform.position);

            // Le damos de nuevo la orden de moverse
            polloagent.SetDestination(chickenGoal.transform.position);
        }
    }

    void ReactivarNavMesh()
    {
        polloagent.enabled = true;
        polloagent.destination = chickenGoal.transform.position;
    }

    // --- TRIGGERS ---
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

    // --- CORRUTINA ---
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