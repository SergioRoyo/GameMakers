using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    public GameObject victory;
    public GameObject interfaz;
    public GameObject coin1;
    public GameObject coin2;
    public GameObject coin3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coin1.SetActive(false);
        coin2.SetActive(false);
        coin3.SetActive(false);
        victory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            foreach (GameObject player in CameraFollow2.Instance.players)
            {

                Destroy(player);
            }
            Destroy(GameObject.FindGameObjectWithTag("Pollo"));
            Finish();
        }
    }


    public void Finish()
    {
        victory.SetActive(true);
        interfaz.SetActive(false);
        if (monedasController.Instance.count == 0)
        {

        }
        else if (monedasController.Instance.count == 1)
        {
            coin1.SetActive(true);
        }
        else if (monedasController.Instance.count == 2)
        {
            coin1.SetActive(true);
            coin2.SetActive(true);

        }
        else
        {
            coin1.SetActive(true);
            coin2.SetActive(true);
            coin3.SetActive(true);
        }

    }

    public void Continue()
    {

        SceneManager.LoadScene(0);
    }
}
