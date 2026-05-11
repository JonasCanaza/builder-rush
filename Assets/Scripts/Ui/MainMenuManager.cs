using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("References Settings")]
    [SerializeField] private MainPanelController mainPanel;
    [SerializeField] private SettingsPanelController settingsPanel;
    [SerializeField] private CreditsPanelController creditsPanel;

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
        mainPanel.gameObject.SetActive(true);
        settingsPanel.gameObject.SetActive(false);
        creditsPanel.gameObject.SetActive(false);
    }

    public void ShowSettingsPanel()
    {
        mainPanel.gameObject.SetActive(false);
        settingsPanel.gameObject.SetActive(true);
        creditsPanel.gameObject.SetActive(false);
    }

    public void ShowCreditsPanel()
    {
        mainPanel.gameObject.SetActive(false);
        settingsPanel.gameObject.SetActive(false);
        creditsPanel.gameObject.SetActive(true);
    }
}