using UnityEngine;

public class FLACO_CONTROLLER : MonoBehaviour
{
    [SerializeField] public GameObject rampaVisual;
    public bool rampaSwitch;

    void Start()
    {
        rampaSwitch = false;
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rampa"))
        {
        rampaSwitch=true;
            rampaVisual = other.transform.GetChild(0).gameObject;
            
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Rampa"))
        {
            other.transform.GetChild(0).gameObject.SetActive(false);
            rampaSwitch = false;
        }
        
    }
    private void OnHabilidad1()
    {
        if (rampaSwitch)
        {
            rampaVisual.SetActive(true);
        }
    }
}
