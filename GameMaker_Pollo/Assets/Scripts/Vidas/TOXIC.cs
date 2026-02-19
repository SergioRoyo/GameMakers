using UnityEngine;
using UnityEngine.SceneManagement;

public class TOXIC : MonoBehaviour
{
    public static TOXIC Instance;
    public Revivir_Controller revivirController;
    public Revivir revivir;
    public int player = 0;
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
        player = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 cielo = this.gameObject.transform.position + (Vector3.up * 90);
            other.transform.position = cielo;
            if (other.name == "Jugador_1")
            {
                GameObject otro = GameObject.Find("Jugador_2");
                revivirController = otro.GetComponent<Revivir_Controller>();
                revivirController.vidasCount--;
                revivirController.muerto = true;
                player = 1;
            }
            else if (other.name == "Jugador_2")
            {
                GameObject otro = GameObject.Find("Jugador_1");
                revivirController = otro.GetComponent<Revivir_Controller>();
                revivirController.vidasCount--;
                revivirController.muerto = true;
                player = 2;
            }
            if (revivirController.vidasCount < 0)
            {
                foreach (GameObject player in RailCameraDriver.Instance.players)
                {
                    Destroy(player);
                }
                Destroy(GameObject.FindGameObjectWithTag("Pollo"));
                SceneManager.LoadScene(0);

            }
        }
        if (other.CompareTag("Pollo"))
        {

            foreach (GameObject player in RailCameraDriver.Instance.players)
            {
                Destroy(player);
            }
            Destroy(GameObject.FindGameObjectWithTag("Pollo"));
            SceneManager.LoadScene(0);

        }

    }
}
