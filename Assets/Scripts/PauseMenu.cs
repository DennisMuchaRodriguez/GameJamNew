using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button audioButton;
    [SerializeField] private GameObject audioMenuPanel;

    [Header("Settings")]
    [SerializeField] private string mainMenuScene = "Menu";
    [SerializeField] private KeyCode pauseKey = KeyCode.O;

    private bool isPaused = false;

    private void Awake()
    {
       
        resumeButton.onClick.AddListener(ResumeGame);
        menuButton.onClick.AddListener(ReturnToMenu);
        audioButton.onClick.AddListener(ToggleAudioMenu);


        pauseMenuPanel.SetActive(false);
        audioMenuPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; 
        pauseMenuPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuPanel.SetActive(false);
        audioMenuPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(mainMenuScene);
    }

    public void ToggleAudioMenu()
    {
        audioMenuPanel.SetActive(!audioMenuPanel.activeSelf);
    }
}