using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("References Settings")]
    [SerializeField] private MainPanelController mainPanel;
    [SerializeField] private SettingsPanelController settingsPanel;
    [SerializeField] private CreditsPanelController creditsPanel;

    [Header("Popups References")]
    [SerializeField] private ExitGamePopup exitGamePopup;

    private void Awake()
    {
        settingsPanel.OnBackPressed += ShowMainPanel;
    }

    private void OnDestroy()
    {
        settingsPanel.OnBackPressed -= ShowMainPanel;
    }

    public void ShowMainPanel()
    {
        mainPanel.Show();
        settingsPanel.Hide();
        creditsPanel.Hide();
    }

    public void ShowSettingsPanel()
    {
        mainPanel.Hide();
        settingsPanel.Show();
        creditsPanel.Hide();
    }

    public void ShowCreditsPanel()
    {
        mainPanel.Hide();
        settingsPanel.Hide();
        creditsPanel.Show();
    }

    public void ShowExitPopup()
    {
        exitGamePopup.Show();
    }
}