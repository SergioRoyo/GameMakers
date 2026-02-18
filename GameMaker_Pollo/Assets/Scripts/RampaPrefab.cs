using UnityEngine;

public class RampaPrefab : MonoBehaviour
{
    [Header("Rampa Settings")]
    [SerializeField] private bool isActive = false;
    [SerializeField] private GameObject rampaVisual; // El objeto que se activa como rampa
    [SerializeField] private float activationTime = 0f; // Tiempo de activación (0 = instantáneo)

    [Header("Activation Settings")]
    [SerializeField] private string activationButton = "Habilidad1"; // Puedes cambiar por "Habilidad1"

    private bool playerInRange = false;
    private FLACO_CONTROLLER flacoController; // Referencia al jugador

    void Start()
    {
        if (rampaVisual != null)
        {
            rampaVisual.SetActive(false); // Inicialmente desactivada
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetButtonDown(activationButton))
        {
            ActivarRampa();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            flacoController = other.GetComponent<FLACO_CONTROLLER>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            flacoController = null;
        }
    }

    public void ActivarRampa()
    {
        if (rampaVisual != null)
        {
            rampaVisual.SetActive(true);
            isActive = true;

            // Si quieres que se desactive después de un tiempo
            if (activationTime > 0)
            {
                Invoke("DesactivarRampa", activationTime);
            }
        }
    }

    public void DesactivarRampa()
    {
        if (rampaVisual != null)
        {
            rampaVisual.SetActive(false);
            isActive = false;
        }
    }

    public bool GetIsActive()
    {
        return isActive;
    }
}
