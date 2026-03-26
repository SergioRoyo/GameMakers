using UnityEngine;
using UnityEngine.SceneManagement;

public class TOXIC : MonoBehaviour
{
    public static TOXIC Instance;
    public Revivir_Controller revivirController;
    public Revivir revivir;
    public int player = 0;
    public bool muerto1 = false;
    public bool muerto2 = false;
    public GameObject cielo;
    public GameObject dead;
    public GameObject interfaz;
    public Chicken_take chicken_Take;
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
        dead.SetActive(false);
        interfaz.SetActive(true);
        muerto1 = false;
        muerto2 = false;
        player = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (muerto1 && muerto2)
        {
            ResetGame();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

                chicken_Take=other.GetComponent<Chicken_take>();
            Vector3 cieloT = cielo.transform.position;
            other.transform.position = cieloT;
            if (other.name == "Jugador_1")
            {
                if (chicken_Take.take)
                {
                    ResetGame();
                }
                else
                {
                    muerto1 = true;
                GameObject otro = GameObject.Find("Jugador_2");
                revivirController = otro.GetComponent<Revivir_Controller>();
                revivirController.vidasCount--;
                revivirController.muerto = true;
                player = 1;

                }
            }
            else if (other.name == "Jugador_2")
            {
                if (chicken_Take.take)
                {
                    ResetGame();
                }
                else
                {
                    muerto2 = true;
                    GameObject otro = GameObject.Find("Jugador_1");
                    revivirController = otro.GetComponent<Revivir_Controller>();
                    revivirController.vidasCount--;
                    revivirController.muerto = true;
                    player = 2;
                }
            }
            if (revivirController.vidasCount < 0)
            {
                ResetGame();

            }
        }
        if (other.CompareTag("Pollo"))
        {

            ResetGame();
        }


    }

    public void ResetGame()
    {
        foreach (GameObject player in CameraFollow2.Instance.players)
        {

            Destroy(player);
        }
        Destroy(GameObject.FindGameObjectWithTag("Pollo"));
        Dead();


    }
    public void Dead()
    {
        dead.SetActive(true);
      interfaz.SetActive(false);
    }
    public void Continue()
    {
       
        SceneManager.LoadScene(0);
    }
}
