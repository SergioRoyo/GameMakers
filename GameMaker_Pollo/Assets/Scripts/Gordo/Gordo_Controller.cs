using System.Collections;
using UnityEngine;

public class Gordo_Controller : MonoBehaviour
{
    public ControladorJugador controladorJugador;
    public float speedMultiply = 5f;
    public float resetSpeed = 5f;
    public bool rodando = true;
    public float rodandoTime = 3f;
    public GameObject habilidad2;
    public bool stayHabilidad2 = false;
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
        if (controladorJugador.isGrounded && Physics.Raycast(transform.position, Vector3.down, controladorJugador.distanciaRayo, controladorJugador.capaSuelo) && rodando)
        {
            StartCoroutine(Rodar());
        }
        
    }
    public void OnHabilidad2()
    {
        if (stayHabilidad2)
        {

            habilidad2.SetActive(true);
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Habilidad2"))
        {
            habilidad2 = other.transform.GetChild(0).gameObject  ;
                 stayHabilidad2 =true;    
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Habilidad2"))
        {
            stayHabilidad2 = false;
            other.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
