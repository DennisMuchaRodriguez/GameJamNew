using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using Unity.Cinemachine;
public class GameManager : MonoBehaviour
{
    [Header("Balance Settings")]
    public Slider balanceSlider;
    public float balanceValue = 0f;
    public float baseDriftSpeed = -0.1f;
    private float currentDriftSpeed;
    public float safeRange = 0.3f;

    [Header("Time Settings")]
    public float totalTime = 60f;
    public TMP_Text timeText;
    private float currentTime;

    [Header("Game Over")]
    public string loseScene = "Derrota";
    public string winScene = "Victoria";
    public Image screenEffect;
    [SerializeField] private SceneTransition scenetranscribe;
    [SerializeField] private CinemachineImpulseSource impulseSource;
    private bool canShake = true;
    [SerializeField] private float shakeCooldown = 10f;

    private bool gameEnded = false;

    void Start()
    {
        InitializeGame();
    }

    void InitializeGame()
    {
        currentTime = totalTime;
        balanceValue = 0f;
        currentDriftSpeed = baseDriftSpeed;
        gameEnded = false;


        if (balanceSlider != null)
        {
            balanceSlider.minValue = -1f;
            balanceSlider.maxValue = 1f;
            balanceSlider.value = 0f;
        }

        if (timeText != null)
        {
            timeText.text = "Tiempo: " + Mathf.Round(currentTime);
        }

        // Efecto visual inicial (rojo tenue)
        if (screenEffect != null)
        {
            screenEffect.color = new Color(0.8f, 0.2f, 0.2f, 0.1f);
        }
    }

    void Update()
    {
        if (gameEnded) return;


        balanceValue = Mathf.Clamp(balanceValue + currentDriftSpeed * Time.deltaTime, -1f, 1f);


        if (balanceSlider != null)
        {
            balanceSlider.value = balanceValue;
        }

        if (Mathf.Abs(balanceValue) <= safeRange)
        {
            currentTime -= Time.deltaTime;
            if (timeText != null)
            {
                timeText.text = "Tiempo: " + Mathf.Round(currentTime);
            }
        }


        if (currentTime <= 0)
        {
            EndGame(true);
        }
        else if (balanceValue <= -1f || balanceValue >= 1f)
        {
            EndGame(false);
        }

        if (balanceValue < -0.75f && canShake)
        {
            if (impulseSource != null)
            {
                impulseSource.GenerateImpulse();
                StartCoroutine(ShakeCooldown());
            }
        }
        if (balanceValue > 0.75f && canShake)
        {
            if (impulseSource != null)
            {
                impulseSource.GenerateImpulse();
                StartCoroutine(ShakeCooldown());
            }
        }
        UpdateScreenEffect();
    }

    void UpdateScreenEffect()
    {
        if (screenEffect == null) return;

        Color targetColor = currentDriftSpeed > 0 ?
            new Color(0.2f, 0.8f, 0.2f, 0.1f) :
            new Color(0.8f, 0.2f, 0.2f, 0.1f);

        screenEffect.color = Color.Lerp(screenEffect.color, targetColor, Time.deltaTime * 5f);
    }

    public void ModificarBalance(float cantidad)
    {
        if (gameEnded) return;


        balanceValue = Mathf.Clamp(balanceValue + cantidad, -1f, 1f);


        if (cantidad > 0)
        {
            currentDriftSpeed = Mathf.Abs(baseDriftSpeed);
        }
        else if (cantidad < 0)
        {
            currentDriftSpeed = -Mathf.Abs(baseDriftSpeed);
        }
    }
    IEnumerator ShakeCooldown()
    {
        canShake = false;
        yield return new WaitForSeconds(shakeCooldown);
        canShake = true;
    }

    void EndGame(bool won)
    {
        if (gameEnded) return;
        gameEnded = true;

        if (won)
        {

            StartCoroutine(GanarConRetraso());
        }
        else
        {
            StartCoroutine(PerderConRetraso());
        }
    }

    IEnumerator PerderConRetraso()
    {

        if (scenetranscribe != null)
        {
            scenetranscribe.LoadSceneWithFade(loseScene);

        }

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(loseScene);
    }
    IEnumerator GanarConRetraso()
    {

        if (scenetranscribe != null)
        {
            scenetranscribe.LoadSceneWithFade(winScene);
        }

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(winScene);
    }
}