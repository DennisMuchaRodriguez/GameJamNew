using UnityEngine;

public class ObjetoMalo : MonoBehaviour
{
    [Header("Configuración")]
    public int puntosBalance = -1; // Puntos que resta al jugador
   

    [SerializeField] private float VelocityZ;
    [SerializeField] private float MinX;
    //[Header("Efectos")]
    //public ParticleSystem efectoVisual;
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
            
            //ReproducirEfectos();

            Destroy(gameObject);
        }
    }

    void ReproducirEfectos()
    {
        //if (efectoVisual != null)
        //{
        //    Instantiate(efectoVisual, transform.position, Quaternion.identity);
        //}
        //    
        //
        //if (sonidoColision != null)
        //{
        //    AudioSource.PlayClipAtPoint(sonidoColision, transform.position);
        //}
        //    
    }
}