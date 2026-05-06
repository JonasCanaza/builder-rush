using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("References Settings")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject settingsPanel;

    public void ShowMainPanel()
    {
        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void ShowSettingsPanel()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
}