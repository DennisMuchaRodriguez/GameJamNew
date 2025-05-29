using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class MenuAnimations : MonoBehaviour
{
    [Header("Título")]
    public RectTransform title;      // Arrastra tu título aquí
    public float titleDropHeight = 100f; // Desde cuánto cae
    public float titleDuration = 0.8f;

    [Header("Botones")]
    public Button[] buttons;        // Arrastra tus botones aquí
    public float buttonDelay = 0.2f; // Retraso entre botones
    public float buttonMoveDuration = 0.6f;
    public float buttonBounce = 0.7f;

    [Header("Imagen")]
    public RectTransform menuImage; // Imagen del lado derecho
    public float imageSlideInDelay = 0.5f;
    public float imageSlideDuration = 1f;

    void Start()
    {
        InitializePositions();
        AnimateMenu();
    }

    void InitializePositions()
    {
        // Título: lo movemos arriba
        title.anchoredPosition += Vector2.up * titleDropHeight;

        // Botones: los movemos abajo
        foreach (var button in buttons)
        {
            var rect = button.GetComponent<RectTransform>();
            rect.anchoredPosition += Vector2.down * 100f;
            button.GetComponent<CanvasGroup>().alpha = 0;
        }

        // Imagen: la movemos a la derecha
        menuImage.anchoredPosition += Vector2.right * 500f;
    }

    void AnimateMenu()
    {
        // 1. Título cae desde arriba
        title.DOAnchorPosY(title.anchoredPosition.y - titleDropHeight, titleDuration)
            .SetEase(Ease.OutBounce);

        // 2. Botones aparecen desde abajo
        for (int i = 0; i < buttons.Length; i++)
        {
            var button = buttons[i];
            var rect = button.GetComponent<RectTransform>();
            var canvasGroup = button.GetComponent<CanvasGroup>();

            float delay = i * buttonDelay;

            // Movimiento + Fade
            rect.DOAnchorPosY(rect.anchoredPosition.y + 100f, buttonMoveDuration)
                .SetDelay(delay)
                .SetEase(Ease.OutBack, buttonBounce);

            canvasGroup.DOFade(1, buttonMoveDuration * 0.8f)
                .SetDelay(delay);
        }

        // 3. Imagen entra desde la derecha
        menuImage.DOAnchorPosX(menuImage.anchoredPosition.x - 500f, imageSlideDuration)
            .SetDelay(imageSlideInDelay)
            .SetEase(Ease.OutBack);
    }

    void OnDestroy()
    {
        DOTween.KillAll(); // Limpieza de animaciones
    }
}