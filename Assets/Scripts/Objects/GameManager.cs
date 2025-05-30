using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("Balance Settings")]
    public Slider balanceSlider;
    public float balanceValue = 0f;        // Empieza en el centro (0)
    public float baseDriftSpeed = -0.1f;  // Velocidad base hacia negativo
    private float currentDriftSpeed;      // Velocidad actual
    public float safeRange = 0.3f;        // Rango seguro para el temporizador

    [Header("Time Settings")]
    public float totalTime = 60f;
    public TMP_Text timeText;
    private float currentTime;

    [Header("Game Over")]
    public string loseScene = "Derrota";
    public string winScene = "Victoria";
    public Image screenEffect;

    private bool gameEnded = false;

    void Start()
    {
        InitializeGame();
    }

    void InitializeGame()
    {
        currentTime = totalTime;
        balanceValue = 0f;
        currentDriftSpeed = baseDriftSpeed; // Comienza moviéndose hacia negativo
        gameEnded = false;

        // Configuración inicial del slider
        if (balanceSlider != null)
        {
            balanceSlider.minValue = -1f;
            balanceSlider.maxValue = 1f;
            balanceSlider.value = 0f;
        }

        // Configurar texto de tiempo
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

        // Movimiento automático continuo en la dirección actual
        balanceValue = Mathf.Clamp(balanceValue + currentDriftSpeed * Time.deltaTime, -1f, 1f);

        // Actualizar slider visual
        if (balanceSlider != null)
        {
            balanceSlider.value = balanceValue;
        }

        // Actualizar temporizador (solo en zona segura)
        if (Mathf.Abs(balanceValue) <= safeRange)
        {
            currentTime -= Time.deltaTime;
            if (timeText != null)
            {
                timeText.text = "Tiempo: " + Mathf.Round(currentTime);
            }
        }

        // Verificar condiciones de fin de juego
        if (currentTime <= 0)
        {
            EndGame(true); // Victoria
        }
        else if (balanceValue <= -1f || balanceValue >= 1f)
        {
            EndGame(false); // Derrota
        }

        // Actualizar efecto visual basado en dirección
        UpdateScreenEffect();
    }

    void UpdateScreenEffect()
    {
        if (screenEffect == null) return;

        Color targetColor = currentDriftSpeed > 0 ?
            new Color(0.2f, 0.8f, 0.2f, 0.1f) : // Verde cuando va a positivo
            new Color(0.8f, 0.2f, 0.2f, 0.1f);  // Rojo cuando va a negativo

        screenEffect.color = Color.Lerp(screenEffect.color, targetColor, Time.deltaTime * 5f);
    }

    public void ModificarBalance(float cantidad)
    {
        if (gameEnded) return;

        // Aplicar cambio al balance
        balanceValue = Mathf.Clamp(balanceValue + cantidad, -1f, 1f);

        // Cambiar dirección del movimiento según el tipo de objeto
        if (cantidad > 0) // Objeto positivo
        {
            currentDriftSpeed = Mathf.Abs(baseDriftSpeed); // Movimiento hacia positivo
        }
        else if (cantidad < 0) // Objeto negativo
        {
            currentDriftSpeed = -Mathf.Abs(baseDriftSpeed); // Movimiento hacia negativo
        }
    }

    void EndGame(bool won)
    {
        if (gameEnded) return;
        gameEnded = true;

        if (won)
        {
            SceneManager.LoadScene(winScene);
        }
        else
        {
            StartCoroutine(PerderConRetraso());
        }
    }

    IEnumerator PerderConRetraso()
    {
        // Efecto visual de derrota
        if (screenEffect != null)
        {
            screenEffect.color = new Color(0.8f, 0.1f, 0.1f, 0.5f);
        }

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(loseScene);
    }
}