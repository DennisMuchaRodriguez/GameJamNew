using UnityEngine;

public class ObjetoBueno : MonoBehaviour
{
    [Header("Configuración")]
    public int puntosBalance = 1; 

    //[Header("Efectos")]
    //public ParticleSystem efectoVisual;

    [SerializeField] private float VelocityZ;
    [SerializeField] private float MinX;
    //public AudioClip sonidoColision;
    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + VelocityZ * Time.deltaTime);
        if (transform.position.z < MinX)
        {
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            GameManager.instance.ModificarBalance(puntosBalance);
           // ReproducirEfectos();

            Destroy(gameObject);
        }
    }

    void ReproducirEfectos()
    {
       // if (efectoVisual != null)
       //     Instantiate(efectoVisual, transform.position, Quaternion.identity);

       // if (sonidoColision != null)
       // {
       //     AudioSource.PlayClipAtPoint(sonidoColision, transform.position);
       // }
            
    }
}