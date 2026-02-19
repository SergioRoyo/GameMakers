using Unity.VisualScripting;
using UnityEngine;

public class FLACO_CONTROLLER : MonoBehaviour
{
    [SerializeField] public GameObject rampaVisual;
    [SerializeField] public GameObject rampaGhost;
    public bool rampaSwitch;
    public ControladorJugador controladorJugador;
    public float H2gordoForce= 20;
    public PhysicsMaterial noFriction;
    public PhysicsMaterial fullFriction;
    public GameObject flacoTraje;
    CapsuleCollider col;
    void Start()
    {
        rampaSwitch = false;
        controladorJugador = GetComponent<ControladorJugador>();
        col =  GetComponent<CapsuleCollider>();
    }

    void OnTriggerEnter(Collider other)
    {

        if (!enabled) return;
        col.material = fullFriction;
        rampaGhost = other.transform.GetChild(1).gameObject;
        rampaGhost.SetActive(true);

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
        rampaGhost.SetActive(false);
        col.material = noFriction;
        flacoTraje.SetActive(true);

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
            rampaGhost.SetActive(false);
            flacoTraje.SetActive(false);
        }
    }
}
