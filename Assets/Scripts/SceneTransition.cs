using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    [Header("Transition Settings")]
    public Image fadeImage;
    public float fadeDuration = 1f;

    public IEnumerator FadeOut()
    {
        fadeImage.gameObject.SetActive(true);
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            fadeImage.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = 1f;
        fadeImage.color = color;
    }

    public IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            fadeImage.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = 0f;
        fadeImage.color = color;
        fadeImage.gameObject.SetActive(false);
    }
}