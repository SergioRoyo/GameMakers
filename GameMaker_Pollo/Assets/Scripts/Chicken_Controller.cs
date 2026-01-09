using UnityEngine;
using UnityEngine.SceneManagement;

public class Chicken_Controller : MonoBehaviour
{
    public enum PolloState { Idle, Held, Scared }
    public PolloState currentstate;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        switch (currentstate)
        {
            case PolloState.Idle:

                Gizmos.color = Color.green;
                break;
            case PolloState.Held:

                break;
            case PolloState.Scared:

                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
