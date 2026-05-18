using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayUIManager : MonoBehaviour
{
    public bool IsPaused { get; private set; }
    public bool CanTogglePause { get; private set; } = true;

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
        restartLevelPopup.OnCancelPressed += EnableTogglePause;

        exitGamePopup.OnCancelPressed += EnableTogglePause;
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
        restartLevelPopup.OnCancelPressed -= EnableTogglePause;

        exitGamePopup.OnCancelPressed -= EnableTogglePause;
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
        CanTogglePause = true;
        pausePanel.Show();
        settingsPanel.Hide();
    }

    private void ShowSettingsPanel()
    {
        CanTogglePause = false;
        settingsPanel.Show();
    }

    #endregion

    #region Popup Navigation

    private void ShowGameOverPopup()
    {
        CanTogglePause = false;
        AudioManager.Instance.PauseMusic();
        Time.timeScale = 0.0f;
        gameOverPopup.ShowResults();
        gameOverPopup.Show();
    }

    private void ShowRestartLevelPopup()
    {
        CanTogglePause = false;
        restartLevelPopup.Show();
    }

    private void ShowExitGamePopup()
    {
        CanTogglePause = false;
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
        CanTogglePause = true;
        IsPaused = false;
        pausePanel.Hide();
        Time.timeScale = 1.0f;
        AudioManager.Instance.ResumeMusic();
    }

    private void EnableTogglePause()
    {
        CanTogglePause = true;
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