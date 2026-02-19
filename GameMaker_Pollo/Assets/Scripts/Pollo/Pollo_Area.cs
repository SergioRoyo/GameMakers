using UnityEngine;

public class Pollo_Area: MonoBehaviour
{
    public GameObject polloArea;
    public float rayDistance = 100f;
    public float radius = 2f; // radio del círculo

    private RaycastHit hit;

    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hit, rayDistance))
        {
            Debug.DrawRay(transform.position, Vector3.down * hit.distance, Color.red);
        }
        polloArea.transform.position = new Vector3(this.gameObject.transform.position.x,polloArea.gameObject.transform.position.y,this.gameObject.transform.position.z);
    }

    void OnDrawGizmos()
    {
        if (hit.collider != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(hit.point, radius);
        }
    }
}
