using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PrediccionTrayectoria : MonoBehaviour
{
    [Header("Configuración")]
    public Transform puntoDisparo;
    public GameObject proyectilPrefab; // <--- AQUÍ ARRASTRAS TU BALA/GRANADA
    public float fuerzaLanzamiento = 15f;
    public int calidadLinea = 30;
    public float espacioEntrePuntos = 0.1f;

    LineRenderer lr;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        // 1. MIENTRAS APRETAMOS: Dibujamos la línea
        // (Usa "Fire1" para Joystick/Mando o MouseButton para ratón)
        if (Input.GetButton("Fire1") || Input.GetMouseButton(0))
        {
            lr.enabled = true;
            DibujarPrediccion();
        }

        // 2. AL SOLTAR: Disparamos y borramos línea
        if (Input.GetButtonUp("Fire1") || Input.GetMouseButtonUp(0))
        {
            lr.enabled = false;
            Lanzar();
        }
    }

    void DibujarPrediccion()
    {
        lr.positionCount = calidadLinea;
        Vector3[] puntos = new Vector3[calidadLinea];
        Vector3 posicionInicial = puntoDisparo.position;
        Vector3 velocidad = puntoDisparo.forward * fuerzaLanzamiento;

        for (int i = 0; i < calidadLinea; i++)
        {
            float tiempo = i * espacioEntrePuntos;
            // Fórmula mágica de física
            Vector3 pos = posicionInicial + (velocidad * tiempo) + (Physics.gravity * 0.5f * tiempo * tiempo);
            puntos[i] = pos;
        }
        lr.SetPositions(puntos);
    }

    void Lanzar()
    {
        // A. Crear el objeto en la posición de la mano
        GameObject nuevoObjeto = Instantiate(proyectilPrefab, puntoDisparo.position, puntoDisparo.rotation);

        // B. Buscar su Rigidbody
        Rigidbody rb = nuevoObjeto.GetComponent<Rigidbody>();

        // C. ¡Importante! Darle EXACTAMENTE la misma velocidad que usamos en la línea
        if (rb != null)
        {
            rb.linearVelocity = puntoDisparo.forward * fuerzaLanzamiento;
        }
    }
}