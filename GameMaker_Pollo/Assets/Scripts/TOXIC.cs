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
        if (other.gameObject.CompareTag("Player")||other.CompareTag("Pollo"))
        {
            SceneManager.LoadScene(0);
        }
    }
}
