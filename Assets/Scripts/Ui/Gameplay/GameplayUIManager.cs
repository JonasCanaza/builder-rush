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
        pausePanel.Show();
        settingsPanel.Hide();
    }

    public void ShowSettingsPanel()
    {
        pausePanel.Hide();
        settingsPanel.Show();
    }

    private void PauseGame()
    {
        AudioManager.Instance.PauseMusic();
        IsPaused = true;
        pausePanel.Show();
        Time.timeScale = 0.0f;
    }

    private void ResumeGame()
    {
        IsPaused = false;
        pausePanel.Hide();
        Time.timeScale = 1.0f;
        AudioManager.Instance.ResumeMusic();
    }

    private void BackToMenu()
    {
        IsPaused = false;
        pausePanel.Hide();
        Time.timeScale = 1.0f;
        AudioManager.Instance.StopMusic();
        SceneManager.LoadScene(AudioManager.MainMenu);
    }
}