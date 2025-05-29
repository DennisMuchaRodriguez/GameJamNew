using UnityEngine;

public class ObjetoInteractivo : MonoBehaviour
{
    [Header("Configuraci�n")]
    public float puntosBalance = 0.1f; // Cantidad m�s peque�a para ajuste gradual
    public float VelocityZ = -5f;
    public float MinX = -10f;

    void Update()
    {
        // Movimiento del objeto
        transform.Translate(0, 0, VelocityZ * Time.deltaTime);

        // Destruir si sale de pantalla
        if (transform.position.z < MinX)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.instance != null)
        {
            GameManager.instance.ModificarBalance(puntosBalance);
            Destroy(gameObject);
        }
    }
}