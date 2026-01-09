using UnityEngine;

public class Chicken_take : MonoBehaviour
{
    public Transform manos;
    public float fuerzaLanzamiento = 5f;

    private GameObject objetoCerca;
    private bool estaCargando = false;

    // VARIABLE CLAVE: Guardamos quién fue el último en sueltar este objeto específico
    // Usamos una variable estática para que todos los pollos sepan quién fue el último "dueño"
    private static GameObject ultimoDueno;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (estaCargando)
            {
                DropChicken();
            }
            // Condición: Si hay un objeto cerca Y (yo no fui el último en soltarlo O nadie lo ha soltado aún)
            else if (objetoCerca != null && ultimoDueno != gameObject)
            {
                TakeChicken();
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
    }

    void DropChicken()
    {
        estaCargando = false;

        // Marcamos a ESTE jugador como el último dueño al soltarlo
        ultimoDueno = gameObject;

        Rigidbody rb = objetoCerca.GetComponent<Rigidbody>();
        Collider col = objetoCerca.GetComponent<Collider>();

        objetoCerca.transform.SetParent(null);
        objetoCerca.transform.position = transform.position + transform.forward * 1.2f;

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.AddForce((Vector3.up + transform.forward * 0.2f) * fuerzaLanzamiento, ForceMode.Impulse);
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
}