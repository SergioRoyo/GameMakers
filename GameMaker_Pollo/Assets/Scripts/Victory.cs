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
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            Finish();
        }
    }
    public void Reset()
    {
        SceneManager.LoadScene(1);
    }
    public void Finish()
    {
        victory.SetActive(true);
       Time.timeScale = 0;
    }
}
