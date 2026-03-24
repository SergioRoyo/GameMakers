using System.Collections;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaCarga : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(CargarScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator CargarScene()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(1);
    }
}
