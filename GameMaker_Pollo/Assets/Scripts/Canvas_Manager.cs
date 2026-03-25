using UnityEngine;
using UnityEngine.UI;

public class Canvas_Manager : MonoBehaviour
{
    public static Canvas_Manager Instance;
    public Slider P1slider;
    public Slider P2slider;
    public GameObject caratulaf1;
    public GameObject caratulaf2;
    public GameObject caratulag1;
    public GameObject caratulag2;
    public bool caratulas = false;

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
       
    }

    // Update is called once per frame
    void Update()
    {
     
        
    }
    public void Cartulas1()
    {
        if(caratulaf1!=null || caratulaf2 != null || caratulag1 != null || caratulag2 != null)
        {

            caratulaf1.SetActive(false);
            caratulaf2.SetActive(true);
            caratulag1.SetActive(true);
            caratulag2.SetActive(false);
        
        }
    }
    public void Cartulas2()
    {
        if (caratulaf1 != null || caratulaf2 != null || caratulag1 != null || caratulag2 != null)
        {
        caratulaf1.SetActive(true);
        caratulaf2.SetActive(false);
        caratulag1.SetActive(false);
        caratulag2.SetActive(true);
        }
    }

    
}
