using UnityEngine;

public class ObjetoInteractivo : MonoBehaviour
{
    [Header("Configuraci�n")]
    public float puntosBalance = 0.2f; // Valores m�s grandes para mayor impacto
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

        // Destruir si pasa la posici�n l�mite
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