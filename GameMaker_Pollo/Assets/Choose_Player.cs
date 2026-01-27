using UnityEngine;
using UnityEngine.InputSystem;

public class SelectorDeJugador : MonoBehaviour
{
    [Header("Configuración")]
    public GameObject modeloJugador1; // Arrastra aquí el objeto "Modelo_P1"
    public GameObject modeloJugador2; // Arrastra aquí el objeto "Modelo_P2"

    void Start()
    {
        // 1. Averiguamos qué número de jugador somos (0, 1, 2...)
        PlayerInput input = GetComponent<PlayerInput>();
        int indice = input.playerIndex;

        // 2. Activamos el modelo correspondiente
        if (indice == 0)
        {
            modeloJugador1.SetActive(true);
            modeloJugador2.SetActive(false);
            // Si necesitas cambiar velocidad o fuerza específica, hazlo aquí
            // GetComponent<MOVIMENTO>().speed = 8; 
        }
        else if (indice == 1)
        {
            modeloJugador1.SetActive(false);
            modeloJugador2.SetActive(true);
            // GetComponent<MOVIMENTO>().speed = 5;
        }
    }
}