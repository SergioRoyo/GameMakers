using UnityEngine;

public class Revivir_Controller : MonoBehaviour
{

    public GameObject almacenVidas;
    public int vidasCount = 3;
    public bool muerto = false;
    public GameObject bengala;
    public Transform manos;
   
    public Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnRevivir()
    {
        if (gameObject.name == "Jugador_1")
        {
            almacenVidas = GameObject.FindWithTag("Vidas1");

        }
        else if (gameObject.name == "Jugador_2")
        {
            almacenVidas = GameObject.FindWithTag("Vidas2");
        }
        if (muerto)
        {
           
            Instantiate(bengala, manos);
            rb = GameObject.FindWithTag("Bengala").GetComponent<Rigidbody>();
            //  rb.linearVelocity = Vector3.zero;
            Vector3 direccionSalto = (transform.up * 2f) + (transform.forward *5.0f);
            rb.AddForce(direccionSalto * 2, ForceMode.Impulse);

            almacenVidas.transform.GetChild(vidasCount).gameObject.SetActive(false);
            
            muerto = false;
        }
    }
}
