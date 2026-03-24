using UnityEngine;
using UnityEngine.AI;

public class JaulaPollo : MonoBehaviour
{
    public static JaulaPollo Instance;
    public GameObject jaula;
    public NavMeshAgent pollo;

    private void Awake()
    {
        if (Instance == null)
        {

            Instance = this;


        }
        else
        {
            // Si ya existe uno, este sobra. ¡Lo destruimos!
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jaula.SetActive(true);
        pollo.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jaula.SetActive(false);
            pollo.enabled = true;
            

        }
    }
}
