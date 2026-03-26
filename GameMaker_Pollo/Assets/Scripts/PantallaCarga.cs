using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PantallaCarga : MonoBehaviour
{
    [Header("Configuración UI")]
    public Slider sliderCarga;
    public float tiempoTotal = 8f; // Ahora es 8, pero funcionará con cualquier número

    [Header("Objetos Aleatorios")]
    public List<GameObject> objetosVisuales;

    void Start()
    {
        // 1. Apagamos todos los objetos al inicio
        foreach (GameObject obj in objetosVisuales) { obj.SetActive(false); }

        // 2. Mezclamos la lista aleatoriamente (Fisher-Yates)
        for (int i = 0; i < objetosVisuales.Count; i++)
        {
            GameObject temporal = objetosVisuales[i];
            int indiceAleatorio = Random.Range(i, objetosVisuales.Count);
            objetosVisuales[i] = objetosVisuales[indiceAleatorio];
            objetosVisuales[indiceAleatorio] = temporal;
        }

        // 3. Iniciamos la corrutina
        StartCoroutine(RutinaDeCarga());
    }

    IEnumerator RutinaDeCarga()
    {
        float tiempoTranscurrido = 0f;
        int indiceActual = -1;

        // Calculamos cuánto tiempo debe estar cada objeto en pantalla
        // Si tiempoTotal es 8 y hay 4 objetos, esto dará 2 segundos por objeto
        float tiempoPorObjeto = tiempoTotal / objetosVisuales.Count;

        while (tiempoTranscurrido < tiempoTotal)
        {
            tiempoTranscurrido += Time.deltaTime;

            // --- Lógica del Índice Dinámico ---
            // Dividimos el tiempo actual por el tiempo que le toca a cada objeto
            int nuevoIndice = Mathf.FloorToInt(tiempoTranscurrido / tiempoPorObjeto);

            // Aseguramos que el índice no se pase del tamaño de la lista
            nuevoIndice = Mathf.Clamp(nuevoIndice, 0, objetosVisuales.Count - 1);

            // Si toca cambiar de objeto...
            if (nuevoIndice != indiceActual)
            {
                // Apagamos el que estaba puesto
                if (indiceActual >= 0) objetosVisuales[indiceActual].SetActive(false);

                // Encendemos el que toca ahora
                objetosVisuales[nuevoIndice].SetActive(true);
                indiceActual = nuevoIndice;
            }

            // --- Actualización del Slider ---
            if (sliderCarga != null)
            {
                sliderCarga.value = tiempoTranscurrido / tiempoTotal;
            }

            yield return null;
        }

        // Al final, cargamos la escena 1
        SceneManager.LoadScene(1);
    }
}