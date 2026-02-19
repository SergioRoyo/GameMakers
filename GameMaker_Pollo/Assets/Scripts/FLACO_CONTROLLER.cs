using Unity.VisualScripting;
using UnityEngine;

public class FLACO_CONTROLLER : MonoBehaviour
{
    [SerializeField] public GameObject rampaVisual;
    public bool rampaSwitch;
    public ControladorJugador controladorJugador;
    public float H2gordoForce= 20;
    void Start()
    {
        rampaSwitch = false;
        controladorJugador = GetComponent<ControladorJugador>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!enabled) return;

        if (other.CompareTag("Rampa"))
        {
        rampaSwitch=true;
            rampaVisual = other.transform.GetChild(0).gameObject;
            
        }
        if (other.CompareTag("H2Gordo"))
        {
            controladorJugador.rb.AddForce(Vector3.up * H2gordoForce, ForceMode.Impulse);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!enabled) return;

        if (other.CompareTag("Rampa"))
        {
            other.transform.GetChild(0).gameObject.SetActive(false);
            rampaSwitch = false;
        }
        
    }
    private void OnHabilidad1()
    {
        if (!enabled) return;

        if (rampaSwitch)
        {
            rampaVisual.SetActive(true);
        }
    }
}
