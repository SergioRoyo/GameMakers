using UnityEngine;
using UnityEngine.EventSystems;

public class DeadButton : MonoBehaviour
{
    public GameObject botonRestart;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
 
    }
    // OnEnable es mágico: se ejecuta en el instante exacto en que tu script TOXIC enciende este Panel
    void OnEnable()
    {
        // Si no hay EventSystem (por error), avisamos en la consola para no romper el juego
        if (EventSystem.current == null)
        {
                        return;
        }

        // 1. Por seguridad, le quitamos el foco a cualquier otra cosa que estuviera seleccionada
        EventSystem.current.SetSelectedGameObject(null);

        // 2. Le decimos al EventSystem: "¡Pon el cursor invisible del mando en este botón!"
        EventSystem.current.SetSelectedGameObject(botonRestart);
    }
}
