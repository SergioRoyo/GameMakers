using UnityEngine;
using UnityEngine.SceneManagement;

public class salir : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void IrAlInicio()
    {
        GameObject otro = GameObject.Find("Jugador_1");
        GameObject otro2 = GameObject.Find("Jugador_2");
        GameObject otro3 = GameObject.Find("pollo");
        Destroy(otro);
        Destroy(otro2);
        Destroy(otro3);

        SceneManager.LoadScene(0);
    }
    public void SalirDelJuego()
    {
        Debug.Log("Cerrando");
        Application.Quit();
    }
}
