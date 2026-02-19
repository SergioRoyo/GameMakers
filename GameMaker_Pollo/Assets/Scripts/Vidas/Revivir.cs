using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class Revivir : MonoBehaviour
{
    public GameObject muerto;

    public float revivirTime = 2f;
    public bool activo=false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()


    {
        activo = false;
        if (TOXIC.Instance.player == 1)
        {
            muerto = GameObject.Find("Jugador_1");
        }
        else if (TOXIC.Instance.player == 2)
        {
            muerto = GameObject.Find("Jugador_2");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision other)
    {
        if (!activo)
        {
        StartCoroutine(RevivirPersonaje());

        }

    }
    IEnumerator RevivirPersonaje()
    {
        activo = true;
        Vector3 zonaAterrizar = this.gameObject.transform.position + (Vector3.up * 10);
        muerto.transform.position = zonaAterrizar;
        muerto = null;
        yield return new WaitForSeconds(revivirTime);

        Destroy(this.gameObject);
    }
}
