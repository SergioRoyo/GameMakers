using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.Splines;
using Unity.VisualScripting;

public class RailCameraDriver : MonoBehaviour
{

    public static RailCameraDriver Instance;

    [Header("Referencias")]
    public CinemachineSplineCart splineCart;
    public SplineContainer splinePath;
    public GameObject[] players;

    [Header("Configuración de Velocidad")]
    public float baseSpeed = 5f;
    public float boostSpeed = 10f;
    public float acceleration = 2f;

    [Header("Límites")]
    public float leadThreshold = 2f;

    private float currentSpeed;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        currentSpeed = baseSpeed;
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    void Update()
    {
        if (splineCart == null || splinePath == null) return;

        float maxPlayerDist = 0f;

        foreach (GameObject player in players)
        {
            if (player == null) continue;

            // Encontrar punto más cercano
            SplineUtility.GetNearestPoint(splinePath.Spline, splinePath.transform.InverseTransformPoint(player.transform.position), out var nearestPoint, out float t);

            float playerDist = t * splinePath.CalculateLength();

            if (playerDist > maxPlayerDist)
            {
                maxPlayerDist = playerDist;
            }
        }

        float distanceGap = maxPlayerDist - splineCart.SplinePosition;

        float targetSpeed = baseSpeed;
        if (distanceGap > leadThreshold)
        {
            targetSpeed = boostSpeed;
        }

        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * acceleration);

        // CORRECCIÓN: Aplicamos el avance a SplinePosition
        splineCart.SplinePosition += currentSpeed * Time.deltaTime;
    }
}