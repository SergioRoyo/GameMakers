using System.Collections;
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (teamChosed && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(1);
        }
        if (PlayerConected==2)
        {
            //activar joystic izqyuierdo, si se mueve una vez team= true, si se mueve otra vez q sea false y asi sucesivamente
        }
        if (team)
        {
             equipos = Equipos.team1;
        }
        else if (!team)
        {
            equipos = Equipos.team1;
        }
            switch (equipos)
            {
                case Equipos.team1:

                //Asignar equipo 1
                teamChosed=true;
                    break;
                case Equipos.team2:
                //Asignar equipo 2
                teamChosed = true;

                break;

            }
    }
    public void NewPlayer(PlayerInput input)
    {
        PlayerConected++;
    }

}
