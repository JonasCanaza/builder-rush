using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayUIManager : MonoBehaviour
{
    public bool IsPaused { get; private set; }

    [Header("Panel Settings")]
    [SerializeField] private PausePanelController pausePanel;
    [SerializeField] private SettingsPanelController settingsPanel;

    private void Start()
    {
        pausePanel.OnResumeButtonClicked += ResumeGame;
        pausePanel.OnSettingsButtonClicked += ShowSettingsPanel;
        pausePanel.OnExitButtonClicked += BackToMenu;

        settingsPanel.OnBackPressed += ShowPausePanel;
    }

    private void OnDestroy()
    {
        pausePanel.OnResumeButtonClicked -= ResumeGame;
        pausePanel.OnSettingsButtonClicked -= ShowSettingsPanel;
        pausePanel.OnExitButtonClicked -= BackToMenu;

        settingsPanel.OnBackPressed -= ShowPausePanel;
    }

    public void ToggleShowPausePanel()
    {
        if (IsPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void ShowPausePanel()
    {
        pausePanel.gameObject.SetActive(true);
        settingsPanel.gameObject.SetActive(false);
    }

    public void ShowSettingsPanel()
    {
        pausePanel.gameObject.SetActive(false);
        settingsPanel.gameObject.SetActive(true);
    }

    private void PauseGame()
    {
        AudioManager.Instance.PauseMusic();
        IsPaused = true;
        pausePanel.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
    }

    private void ResumeGame()
    {
        IsPaused = false;
        pausePanel.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        AudioManager.Instance.ResumeMusic();
    }

    private void BackToMenu()
    {
        IsPaused = false;
        pausePanel.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        AudioManager.Instance.StopMusic();
        SceneManager.LoadScene(AudioManager.MainMenu);
    }
}