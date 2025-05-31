using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject panelInformacion;
    public GameObject panelCreditos;
    public GameObject panelMusica;

    public void MostrarInformacion(bool mostrar)
    {
        if (panelInformacion != null)
            panelInformacion.SetActive(mostrar);
    }

    public void MostrarCreditos(bool mostrar)
    {
        if (panelCreditos != null)
            panelCreditos.SetActive(mostrar);
    }

    public void MostrarMusica(bool mostrar)
    {
        if (panelMusica != null)
            panelMusica.SetActive(mostrar);
    }
}