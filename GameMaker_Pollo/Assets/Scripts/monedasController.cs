using UnityEngine;

public class monedasController : MonoBehaviour
{
    public static monedasController Instance;
    public int count = 0;
    public GameObject moneda1;
    public GameObject moneda2;
    public GameObject moneda3;
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
        moneda1.SetActive(true);
        moneda2.SetActive(true);
        moneda3.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Count()
    {
        count++;
    }
    
}
