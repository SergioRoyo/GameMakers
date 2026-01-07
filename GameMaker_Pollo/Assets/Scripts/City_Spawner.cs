using UnityEngine;

public class City_Spawner : MonoBehaviour
{
    public Transform jugador;          // Tu bola
    public GameObject[] listaDeSuelos;
    public GameObject[] listaDeEdificios;

    public float longitudSuelo = 10f;  // Lo que mide tu suelo de largo (Z)

    float spawnZ = 0f;                 // Dónde poner la siguiente pieza
    public int cantidadInicial = 10;           // Cuántos suelos crear al arrancar

    void Start()
    {
        // Al empezar, creamos 5 suelos seguidos
        for (int i = 0; i < cantidadInicial; i++)
        {
            GenerarSuelo();
        }
    }

    //void Update()
    //{

    //    if (jugador.position.z > spawnZ - (longitudSuelo * 3))
    //    {
    //        GenerarSuelo();
    //    }
    //}


    void GenerarSuelo()
    {

        int indiceAleatorio = Random.Range(0, listaDeSuelos.Length);
        int indiceAleatorio2 = Random.Range(0, listaDeEdificios.Length);


        Instantiate(listaDeSuelos[indiceAleatorio], new Vector3(0, 0, spawnZ), listaDeSuelos[indiceAleatorio].transform.rotation);
        Instantiate(listaDeEdificios[indiceAleatorio2], new Vector3(0, 0, spawnZ), listaDeEdificios[indiceAleatorio2].transform.rotation);


        spawnZ += longitudSuelo;
    }
}
