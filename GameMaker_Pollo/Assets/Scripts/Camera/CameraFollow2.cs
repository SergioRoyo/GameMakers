using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.Splines;

public class CameraFollow2 : MonoBehaviour
{
    // Mantenemos el Singleton para que tus otros scripts sigan funcionando
    public static CameraFollow2 Instance;

    [Header("Referencias")]
    public CinemachineSplineCart splineCart;
    public SplineContainer splinePath;
    public GameObject[] players;

    [Header("Configuración")]
    public float smoothness = 3f; // Suavizado del movimiento

    private void Awake()
    {
        // Configuración básica del Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Buscamos a los jugadores con el Tag "Player" al iniciar
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    void Update()
    {
        // Si falta algo esencial, detenemos el proceso para evitar errores
        if (splineCart == null || splinePath == null || players.Length == 0) return;

        float sumaDistancias = 0f;
        int conteoJugadores = 0;

        // 1. Sumamos la posición en el Spline de cada jugador
        foreach (GameObject player in players)
        {
            if (player == null) continue;

            // Buscamos el punto más cercano del jugador en el Spline
            SplineUtility.GetNearestPoint(splinePath.Spline, splinePath.transform.InverseTransformPoint(player.transform.position), out var nearestPoint, out float t);

            // Convertimos el progreso (0 a 1) en distancia (metros)
            float playerDist = t * splinePath.CalculateLength();

            sumaDistancias += playerDist;
            conteoJugadores++;
        }

        // 2. Calculamos el punto medio exacto
        float puntoMedio = sumaDistancias / conteoJugadores;

        // 3. Movemos el Cart hacia ese punto medio (funciona hacia adelante y hacia atrás)
        // El Lerp hace que el movimiento sea fluido y no seco
        splineCart.SplinePosition = Mathf.Lerp(splineCart.SplinePosition, puntoMedio, Time.deltaTime * smoothness);
    }
}