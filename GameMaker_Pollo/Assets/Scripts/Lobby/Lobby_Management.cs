using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Lobby_Management : MonoBehaviour
{
    public int PlayerConected = 0;
    public bool  team=false;
    public bool teamChosed = false;
    public enum Equipos { team1, team2 }
    public Equipos equipos;

    public GameObject team1panel;
    public GameObject team2panel;
    public GameObject p1;
    public GameObject p2;


    private List<ControladorJugador> listaJugadores = new List<ControladorJugador>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        p1.SetActive(false);
        p2.SetActive(false);

     

    }

    // Update is called once per frame
    void Update()
    {
        //if (teamChosed && Input.GetKeyDown(KeyCode.Return))
        //{
        //    SceneManager.LoadScene(1);
        //}
        if (PlayerConected==2)
        {
            p2.SetActive(true);
            //activar joystic izqyuierdo, si se mueve una vez team= true, si se mueve otra vez q sea false y asi sucesivamente
            GestionarInputJugador1();
            GestionarLogicaEquipos();
        }
        if (PlayerConected==1) { p1.SetActive(true);  }
        //if (team)
        //{
        //     equipos = Equipos.team1;
        //}
        //else if (!team)
        //{
        //    equipos = Equipos.team1;
        //}
        //    switch (equipos)
        //    {
        //        case Equipos.team1:

        //        //Asignar equipo 1
        //        teamChosed=true;
        //            break;
        //        case Equipos.team2:
        //        //Asignar equipo 2
        //        teamChosed = true;

        //        break;

        //    }
    }
    void GestionarInputJugador1()
    {
        // 1. Accedemos al input del PRIMER jugador de la lista (P1)
        PlayerInput inputP1 = listaJugadores[0].GetComponent<PlayerInput>();

        // 2. Leemos el Stick Izquierdo (Acción "Move")
        Vector2 movimiento = inputP1.actions["Move"].ReadValue<Vector2>();

        // Si mueve a la IZQUIERDA <-
        if (movimiento.x < -0.5f)
        {
            team = false;
            team1panel.SetActive(true);
            team2panel.SetActive(false);
        }
        // Si mueve a la DERECHA ->
        else if (movimiento.x > 0.5f)
        {
            team = true;
            team1panel.SetActive(false);
            team2panel.SetActive(true);
        }

        // 3. Detectar botón para Iniciar (Botón Sur / A / X)
        // Usamos la acción "Jump" 
        if (inputP1.actions["Jump"].WasPressedThisFrame())
        {
            if (teamChosed)
            {
                EmpezarJuego();
            }
        }
    }

    void GestionarLogicaEquipos()
    {
        if (!team)
        {
            equipos = Equipos.team1;
        }
        else 
        {
            equipos = Equipos.team2;
        }

        switch (equipos)
        {
            case Equipos.team1:
                // CASO A: P1 Gordo, P2 Flaco
                listaJugadores[0].PonerseTrajeGordo();
                listaJugadores[1].PonerseTrajeFlaco();

                teamChosed = true;
                break;

            case Equipos.team2:
                // CASO B: P1 Flaco, P2 Gordo
                listaJugadores[0].PonerseTrajeFlaco();
                listaJugadores[1].PonerseTrajeGordo();

                teamChosed = true;
                break;
        }
    }

    void EmpezarJuego()
    {
        SceneManager.LoadScene(1);
    }

    public void NewPlayer(PlayerInput input)
    {
        PlayerConected++;
        input.gameObject.name = "Jugador_" + PlayerConected;
        listaJugadores.Add(input.GetComponent<ControladorJugador>());
        DontDestroyOnLoad(input.gameObject);
    }

}
