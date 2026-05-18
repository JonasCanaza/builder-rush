using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels References")]
    [SerializeField] private MainPanelController mainPanel;
    [SerializeField] private SettingsPanelController settingsPanel;
    [SerializeField] private CreditsPanelController creditsPanel;

    [Header("Popup Reference")]
    [SerializeField] private ExitGamePopup exitGamePopup;

    private void Awake()
    {
        SubscribeEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    #region Event Subscriptions

    private void SubscribeEvents()
    {
        mainPanel.OnPlayPressed += HandlePlayPressed;
        mainPanel.OnSettingsPressed += ShowSettingsPanel;
        mainPanel.OnCreditsPressed += ShowCreditsPanel;
        mainPanel.OnExitPressed += ShowExitPopup;

        settingsPanel.OnBackPressed += ShowMainPanel;

        creditsPanel.OnBackPressed += ShowMainPanel;

        exitGamePopup.OnExitPressed += HandleExitPressed;
    }

    private void UnsubscribeEvents()
    {
        mainPanel.OnPlayPressed -= HandlePlayPressed;
        mainPanel.OnSettingsPressed -= ShowSettingsPanel;
        mainPanel.OnCreditsPressed -= ShowCreditsPanel;
        mainPanel.OnExitPressed -= ShowExitPopup;

        settingsPanel.OnBackPressed -= ShowMainPanel;

        creditsPanel.OnBackPressed -= ShowMainPanel;

        exitGamePopup.OnExitPressed -= HandleExitPressed;
    }

    #endregion

    #region Panel Navigation

    private void ShowMainPanel()
    {
        mainPanel.Show();
        settingsPanel.Hide();
        creditsPanel.Hide();
    }

    private void ShowSettingsPanel()
    {
        mainPanel.Hide();
        settingsPanel.Show();
        creditsPanel.Hide();
    }

    private void ShowCreditsPanel()
    {
        mainPanel.Hide();
        settingsPanel.Hide();
        creditsPanel.Show();
    }

    #endregion

    #region Popup Navigation

    private void ShowExitPopup()
    {
        exitGamePopup.Show();
    }

    #endregion

    #region Event Handlers

    private void HandlePlayPressed()
    {
        SceneManager.LoadScene("SCN_Gameplay");
    }

    private void HandleExitPressed()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    #endregion
}