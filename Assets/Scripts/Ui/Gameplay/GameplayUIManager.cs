using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayUIManager : MonoBehaviour
{
    public bool IsPaused { get; private set; }

    [Header("Panels References")]
    [SerializeField] private PausePanelController pausePanel;
    [SerializeField] private SettingsPanelController settingsPanel;

    [Header("Popups References")]
    [SerializeField] private RestartLevelPopup restartLevelPopup;
    [SerializeField] private ExitLevelPopup exitGamePopup;
    [SerializeField] private GameOverPopup gameOverPopup;

    private void Awake()
    {
        SubscribeEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    #region Event Subscription

    private void SubscribeEvents()
    {
        pausePanel.OnResumePressed += ResumeGame;
        pausePanel.OnRestartPressed += ShowRestartLevelPopup;
        pausePanel.OnSettingsPressed += ShowSettingsPanel;
        pausePanel.OnExitPressed += ShowExitGamePopup;

        settingsPanel.OnBackPressed += ShowPausePanel;

        GameManager.Instance.OnGameOver += ShowGameOverPopup;
        gameOverPopup.OnRestartPressed += RestartLevel;
        gameOverPopup.OnExitPressed += BackToMenu;

        restartLevelPopup.OnRestartPressed += RestartLevel;

        exitGamePopup.OnExitPressed += BackToMenu;
    }

    private void UnsubscribeEvents()
    {
        pausePanel.OnResumePressed -= ResumeGame;
        pausePanel.OnRestartPressed -= ShowRestartLevelPopup;
        pausePanel.OnSettingsPressed -= ShowSettingsPanel;
        pausePanel.OnExitPressed -= ShowExitGamePopup;

        settingsPanel.OnBackPressed -= ShowPausePanel;

        if (GameManager.Instance)
        {
            GameManager.Instance.OnGameOver -= ShowGameOverPopup;
        }

        gameOverPopup.OnRestartPressed -= RestartLevel;
        gameOverPopup.OnExitPressed -= BackToMenu;

        restartLevelPopup.OnRestartPressed -= RestartLevel;

        exitGamePopup.OnExitPressed -= BackToMenu;
    }

    #endregion

    #region Pause Control

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

    #endregion

    #region Panel Navigation

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

    #endregion

    #region Popup Navigation

    private void ShowGameOverPopup()
    {
        AudioManager.Instance.PauseMusic();
        Time.timeScale = 0.0f;
        gameOverPopup.ShowResults();
        gameOverPopup.Show();
    }

    private void ShowRestartLevelPopup()
    {
        restartLevelPopup.Show();
    }

    private void ShowExitGamePopup()
    {
        exitGamePopup.Show();
    }

    #endregion

    #region Pause Management

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

    #endregion

    #region Scene Management

    private void RestartLevel()
    {
        Time.timeScale = 1.0f;
        AudioManager.Instance.StopMusic();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void BackToMenu()
    {
        IsPaused = false;
        pausePanel.Hide();
        Time.timeScale = 1.0f;
        AudioManager.Instance.StopMusic();
        SceneManager.LoadScene(AudioManager.MainMenu);
    }

    #endregion
}