using UnityEngine;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject panelInformacion;
    public GameObject panelCreditos;
    public GameObject panelMusica;
    public GameObject panelObjetos;

    [Header("Referencias RectTransform")]
    public RectTransform panelInfo;
    public RectTransform panelCred;
    public RectTransform panelMusic;
    public RectTransform panelObjets;

    [Header("Animación")]
    public float duracion = 0.5f;
    public float distanciaSalida = 1000f;

    public void MostrarInformacion(bool mostrar)
    {
        if (panelInformacion == null || panelInfo == null) return;

        panelInformacion.SetActive(true);
        if (mostrar)
        {
            panelInfo.anchoredPosition = new Vector2(distanciaSalida, 0);
            panelInfo.DOAnchorPos(Vector2.zero, duracion).SetEase(Ease.OutBack);
        }
        else
        {
            panelInfo.DOAnchorPos(new Vector2(distanciaSalida, 0), duracion)
                     .SetEase(Ease.InBack)
                     .OnComplete(() => panelInformacion.SetActive(false));
        }
    }
    public void MostrarObjetos (bool mostrar)
    {
        if (panelObjetos == null || panelObjets == null) return;

        panelObjetos.SetActive(true);
        if (mostrar)
        {
            panelObjets.anchoredPosition = new Vector2(distanciaSalida, 0);
            panelObjets.DOAnchorPos(Vector2.zero, duracion).SetEase(Ease.OutBack);
        }
        else
        {
             panelObjets.DOAnchorPos(new Vector2(distanciaSalida, 0), duracion)
                     .SetEase(Ease.InBack)
                     .OnComplete(() => panelObjetos.SetActive(false));
        }
    }

    public void MostrarCreditos(bool mostrar)
    {
        if (panelCreditos == null || panelCred == null) return;

        panelCreditos.SetActive(true);
        if (mostrar)
        {
            panelCred.anchoredPosition = new Vector2(-distanciaSalida, 0);
            panelCred.DOAnchorPos(Vector2.zero, duracion).SetEase(Ease.OutBack);
        }
        else
        {
            panelCred.DOAnchorPos(new Vector2(-distanciaSalida, 0), duracion)
                    .SetEase(Ease.InBack)
                    .OnComplete(() => panelCreditos.SetActive(false));
        }
    }

    public void MostrarMusica(bool mostrar)
    {
        if (panelMusica == null || panelMusic == null) return;

        panelMusica.SetActive(true);
        if (mostrar)
        {
            panelMusic.anchoredPosition = new Vector2(0, distanciaSalida);
            panelMusic.DOAnchorPos(Vector2.zero, duracion).SetEase(Ease.OutBack);
        }
        else
        {
            panelMusic.DOAnchorPos(new Vector2(0, distanciaSalida), duracion)
                      .SetEase(Ease.InBack)
                      .OnComplete(() => panelMusica.SetActive(false));
        }
    }
}