using UnityEngine;

public class matar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        if (TOXIC.Instance.muerto1 && TOXIC.Instance.muerto2)
        {
            TOXIC.Instance.ResetGame();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            Vector3 cieloT = TOXIC.Instance.cielo.transform.position;
            other.transform.position = cieloT;
            if (other.name == "Jugador_1")
            {
                TOXIC.Instance.muerto1 = true;
                GameObject otro = GameObject.Find("Jugador_2");
                TOXIC.Instance.revivirController = otro.GetComponent<Revivir_Controller>();
                TOXIC.Instance.revivirController.vidasCount--;
                TOXIC.Instance.revivirController.muerto = true;
                TOXIC.Instance.player = 1;
            }
            else if (other.name == "Jugador_2")
            {
                TOXIC.Instance.muerto2 = true;
                GameObject otro = GameObject.Find("Jugador_1");
                TOXIC.Instance.revivirController = otro.GetComponent<Revivir_Controller>();
                TOXIC.Instance.revivirController.vidasCount--;
                TOXIC.Instance.revivirController.muerto = true;
                TOXIC.Instance.player = 2;
            }
            if (TOXIC.Instance.revivirController.vidasCount < 0)
            {
                TOXIC.Instance.ResetGame();

            }
        }
        if (other.CompareTag("Pollo"))
        {

            TOXIC.Instance.ResetGame();
        }
    }
}
