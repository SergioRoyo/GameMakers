using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum States  { chasing, nochasing,patroll };
public class rAYCAST : MonoBehaviour
{

    [SerializeField]
    float rayDistance;
    public States ActualState;
    public States sdgdsfgdsfg;
    public States sdgdsfgdssdfasdfasdffg;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit,rayDistance))
        {
            
            if (hit.collider.CompareTag("Player"))

            {

                    Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.red);
                
                ActualState = States.chasing;
            }
        }

        switch (ActualState)

        {
            case States.chasing:
                break;
            case States.nochasing:
                break;
            case States.patroll:
                break;
        }
        
    }
    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
       
    //    Gizmos.DrawLine(transform.position, transform.forward);
    //}
}
