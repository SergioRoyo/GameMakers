using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chicken_Explosion : MonoBehaviour
{
    public Color[] Listacolores;
    public int Colores = 0;
    public int Tiempo = 0;
    public Chicken_take chicken_Take;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(changeColor());
        }
    }
    
    
    IEnumerator changeColor()
    {
        yield return new WaitForSeconds(1f);
        if (Tiempo == 5)
        {
            chicken_Take.DropChicken();
            this.GetComponent<Renderer>().material.color = Listacolores[0];

        }
        else
        {
            this.GetComponent<Renderer>().material.color = Listacolores[Colores ++];
        Tiempo++;

            StartCoroutine(changeColor());
        }


       
    }
}
