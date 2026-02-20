using UnityEngine;

public class Game_Spawn : MonoBehaviour
{
    // En el inspector pondrás 1 para el Jugador 1, o 2 para el Jugador 2
    public int numeroDeJugador;

    void Start()
    {
        // 1. Buscamos al jugador por el nombre que le pusimos en el Lobby
        // (El nombre debe ser exacto: "Jugador_1" o "Jugador_2")
        GameObject jugador = GameObject.Find("Jugador_" + numeroDeJugador);

        if (jugador != null)
        {
            // 2. Quitamos la velocidad para que no salga disparado
            Rigidbody rb = jugador.GetComponent<Rigidbody>();
            if (rb != null) rb.linearVelocity = Vector3.zero;

            // 3. Lo traemos a la posición de este objeto
            jugador.transform.position = transform.position;
            jugador.transform.rotation = transform.rotation;
        }
    }
}