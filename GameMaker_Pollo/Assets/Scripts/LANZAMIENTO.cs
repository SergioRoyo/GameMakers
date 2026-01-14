using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(LineRenderer))]
public class PrediccionTrayectoria : MonoBehaviour
{
    [Header("Conexión con Controles")]
    public InputActionReference accionApuntar;

    [Header("Configuración")]
    public Transform puntoDisparo;
    public GameObject proyectilPrefab;
    public float fuerzaMaxima = 30f;
    [Range(0f, 2f)] public float alturaTiro = 0.5f; // <--- NUEVA VARIABLE PARA EL ARCO

    public int calidadLinea = 50;
    public float espacioEntrePuntos = 0.05f;

    LineRenderer lr;
    bool estabaApuntando = false;
    float fuerzaParaDisparar = 0f;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
        if (accionApuntar != null) accionApuntar.action.Enable();
    }

    void Update()
    {
        if (accionApuntar == null) return;

        float presion = accionApuntar.action.ReadValue<float>();

        if (presion > 0.1f)
        {
            lr.enabled = true;
            estabaApuntando = true;
            fuerzaParaDisparar = fuerzaMaxima * presion;

            DibujarPrediccion(fuerzaParaDisparar);
        }
        else if (estabaApuntando)
        {
            lr.enabled = false;
            Lanzar(fuerzaParaDisparar);
            estabaApuntando = false;
            fuerzaParaDisparar = 0f;
        }
    }

    // Función auxiliar para calcular la dirección con curva hacia arriba
    Vector3 CalcularVelocidad(float fuerza)
    {
        // Mezclamos: Ir hacia adelante + Ir hacia arriba
        Vector3 direccion = puntoDisparo.forward + (Vector3.up * alturaTiro);

        // .normalized es importante para que la 'altura' no cambie la velocidad total
        return direccion.normalized * fuerza;
    }

    void DibujarPrediccion(float fuerza)
    {
        lr.positionCount = calidadLinea;
        Vector3[] puntos = new Vector3[calidadLinea];
        Vector3 posicionInicial = puntoDisparo.position;

        // USAMOS LA NUEVA FÓRMULA
        Vector3 velocidad = CalcularVelocidad(fuerza);

        for (int i = 0; i < calidadLinea; i++)
        {
            float tiempo = i * espacioEntrePuntos;
            Vector3 pos = posicionInicial + (velocidad * tiempo) + (Physics.gravity * 0.5f * tiempo * tiempo);
            puntos[i] = pos;
        }
        lr.SetPositions(puntos);
    }

    void Lanzar(float fuerza)
    {
        GameObject nuevoObjeto = Instantiate(proyectilPrefab, puntoDisparo.position, puntoDisparo.rotation);
        Rigidbody rb = nuevoObjeto.GetComponent<Rigidbody>();

        // --- SOLUCIÓN COLISIÓN ---
        // Buscamos el collider del jugador (tu propio cuerpo)
        Collider miCuerpo = GetComponent<Collider>();
        // Si no tienes collider normal, prueba con CharacterController
        if (miCuerpo == null) miCuerpo = GetComponent<CharacterController>();

        Collider cuerpoBala = nuevoObjeto.GetComponent<Collider>();

        // Le decimos a Unity: "Ignora el choque entre estos dos"
        if (miCuerpo != null && cuerpoBala != null)
        {
            Physics.IgnoreCollision(miCuerpo, cuerpoBala);
        }
        // -------------------------

        if (rb != null)
        {
            rb.linearVelocity = CalcularVelocidad(fuerza);
        }
    }
}