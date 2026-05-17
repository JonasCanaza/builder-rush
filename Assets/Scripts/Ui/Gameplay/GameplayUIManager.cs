using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayUIManager : MonoBehaviour
{
    public bool IsPaused { get; private set; }

    [Header("Panel Settings")]
    [SerializeField] private PausePanelController pausePanel;
    [SerializeField] private SettingsPanelController settingsPanel;

    [Header("Popups References")]
    [SerializeField] private RestartLevelPopup restartLevelPopup;
    [SerializeField] private ExitLevelPopup exitGamePopup;

    private void Start()
    {
        pausePanel.OnResumeButtonClicked += ResumeGame;
        pausePanel.OnRestartButtonClicked += ShowRestartLevelPopup;
        pausePanel.OnSettingsButtonClicked += ShowSettingsPanel;
        pausePanel.OnExitButtonClicked += ShowExitGamePopup;

        settingsPanel.OnBackPressed += ShowPausePanel;

        restartLevelPopup.OnRestartConfirmed += RestartLevel;

        exitGamePopup.OnExitConfirmed += BackToMenu;
    }

    private void OnDestroy()
    {
        pausePanel.OnResumeButtonClicked -= ResumeGame;
        pausePanel.OnRestartButtonClicked -= ShowRestartLevelPopup;
        pausePanel.OnSettingsButtonClicked -= ShowSettingsPanel;
        pausePanel.OnExitButtonClicked -= ShowExitGamePopup;

        settingsPanel.OnBackPressed -= ShowPausePanel;

        restartLevelPopup.OnRestartConfirmed -= RestartLevel;

        exitGamePopup.OnExitConfirmed -= BackToMenu;
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

    private void ShowPausePanel()
    {
        pausePanel.Show();
        settingsPanel.Hide();
    }

    private void ShowSettingsPanel()
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

    private void RestartLevel()
    {
        Time.timeScale = 1.0f;
        AudioManager.Instance.StopMusic();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ShowRestartLevelPopup()
    {
        restartLevelPopup.Show();
    }

    private void ShowExitGamePopup()
    {
        exitGamePopup.Show();
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