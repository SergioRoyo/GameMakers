using UnityEngine;

public class CHAsing : MonoBehaviour
{
    public enum StateMachine { idle, chasing, dead };
    public StateMachine states;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        switch (states)
        {
            case StateMachine.idle:

                Debug.Log("idle");
              

                break;

            case StateMachine.chasing:

                Debug.Log("chasing");
     

                break;
            case StateMachine.dead:

                Debug.Log("dead");


                break;

        }
    }
    private void OnDrawGizmos()
    {
        switch (states)
        {
            case StateMachine.idle:

                Gizmos.color = Color.green;
                Gizmos.DrawLine(this.gameObject.transform.position, this.gameObject.transform.position * 1.2f);
                Gizmos.DrawWireSphere(this.gameObject.transform.position, 2);

                break;

            case StateMachine.chasing:

        
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(this.gameObject.transform.position, this.gameObject.transform.position * 1.2f);
                Gizmos.DrawWireSphere(this.gameObject.transform.position, 2);

                break;
            case StateMachine.dead:

                Debug.Log("dead");


                break;

        }
    }

}
