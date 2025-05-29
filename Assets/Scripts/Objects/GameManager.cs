using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Balance Settings")]
    public Slider balanceSlider;
    public float balanceValue = 0.5f;
    public float balanceSpeed = 0.5f;
    public float safeRange = 0.3f;

    [Header("Time Settings")]
    public float totalTime = 60f;
    public TMP_Text timeText;
    private float currentTime;

    [Header("Game Over")]
    public string loseScene = "Derrota";
    public string winScene = "Victoria";
    public Image screenEffect;

    private bool gameEnded = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            InitializeGame();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
     
        FindUIReferences();

      
        if (!scene.name.Equals(loseScene) && !scene.name.Equals(winScene))
        {
            InitializeGame();
        }
    }

    void FindUIReferences()
    {
  
        balanceSlider = GameObject.FindWithTag("BalanceSlider")?.GetComponent<Slider>();
        timeText = GameObject.FindWithTag("TimeText")?.GetComponent<TMP_Text>();
        screenEffect = GameObject.FindWithTag("ScreenEffect")?.GetComponent<Image>();

        if (balanceSlider != null)
        {
            balanceSlider.minValue = -1f;
            balanceSlider.maxValue = 1f;
            balanceSlider.value = balanceValue;
        }
    }

    void InitializeGame()
    {
        currentTime = totalTime;
        balanceValue = 0f;
        gameEnded = false;

        if (timeText != null)
        {
            timeText.text = "Tiempo: " + Mathf.Round(currentTime);
        }

        if (balanceSlider != null)
        {
            balanceSlider.value = 0f;
        }

        if (screenEffect != null)
        {
            screenEffect.color = Color.clear;
        }
    }

    void Update()
    {
        if (gameEnded) return;

     
        if (balanceSlider != null)
        {
            balanceSlider.value = Mathf.Lerp(balanceSlider.value, balanceValue, balanceSpeed * Time.deltaTime);
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

       
        UpdateScreenEffects();
    }

    void UpdateScreenEffects()
    {
        if (screenEffect == null) return;

        if (balanceValue < -safeRange)
        {
            screenEffect.color = Color.Lerp(screenEffect.color,
                new Color(0.8f, 0.2f, 0.2f, 0.1f), Time.deltaTime * 2f);
        }
        else if (balanceValue > safeRange)
        {
            screenEffect.color = Color.Lerp(screenEffect.color,
                new Color(0.2f, 0.2f, 0.8f, 0.1f), Time.deltaTime * 2f);
        }
        else
        {
            screenEffect.color = Color.Lerp(screenEffect.color,
                Color.clear, Time.deltaTime * 2f);
        }
    }

    void EndGame(bool won)
    {
        gameEnded = true;
        CambiarEscena(won ? winScene : loseScene);
    }

    public void ModificarBalance(float cantidad)
    {
        if (gameEnded) return;
        balanceValue = Mathf.Clamp(balanceValue + cantidad, -1f, 1f);
    }

    public void CambiarEscena(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}