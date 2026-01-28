using UnityEngine;
using UnityEngine.SceneManagement;

public class TOXIC : MonoBehaviour
{
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")||other.CompareTag("Pollo"))
        {
            foreach(GameObject player in RailCameraDriver.Instance.players)
            {
                Destroy(player);
            }
            Destroy(GameObject.FindGameObjectWithTag("Pollo"));
            SceneManager.LoadScene(0);
            
        }
    }
}
