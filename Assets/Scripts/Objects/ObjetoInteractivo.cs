using UnityEngine;

public class ObjetoInteractivo : MonoBehaviour
{
    [Header("Configuración")]
    public float puntosBalance = 0.2f; // Valores más grandes para mayor impacto
    public float moveSpeed = -5f;
    public float destroyPositionZ = -10f;

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        // Movimiento hacia el jugador
        transform.Translate(0, 0, moveSpeed * Time.deltaTime);

        // Destruir si pasa la posición límite
        if (transform.position.z < destroyPositionZ)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager != null)
        {
            gameManager.ModificarBalance(puntosBalance);
            Destroy(gameObject);
        }
    }
}