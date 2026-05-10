using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayUIManager : MonoBehaviour
{
    public bool IsPaused { get; private set; }

    [Header("Panel Settings")]
    [SerializeField] private PausePanelController pausePanel;

    private void OnEnable()
    {
        pausePanel.OnResume += ResumeGame;
        pausePanel.OnExit += BackToMenu;
    }

    private void OnDestroy()
    {
        pausePanel.OnResume -= ResumeGame;
        pausePanel.OnExit -= BackToMenu;
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