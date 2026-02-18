using System.Collections;
using UnityEngine;

public class Gordo_Controller : MonoBehaviour
{
    public ControladorJugador controladorJugador;
    public float speedMultiply = 5f;
    public float resetSpeed = 5f;
    public bool rodando = true;
    public float rodandoTime = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnHabilidad1() //Habilidad de rodar
    {
        if (rodando)
        {
            StartCoroutine(Rodar());
        }
    }
    public IEnumerator Rodar()
    {
        rodando = false;
        controladorJugador.speed = controladorJugador.speed * speedMultiply;
        yield return new WaitForSeconds(rodandoTime);
        controladorJugador.speed = resetSpeed;
        rodando = true;
    }
}
