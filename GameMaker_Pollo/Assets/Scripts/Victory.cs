using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    public GameObject victory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
    }
    
    public void Continue()
    {
       
        SceneManager.LoadScene(0);
    }
}
