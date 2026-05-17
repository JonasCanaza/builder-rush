using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainPanelController : UIPanel
{
    [Header("References Settings")]
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonSettings;
    [SerializeField] private Button buttonCredits;
    [SerializeField] private Button buttonExit;
    [SerializeField] private MainMenuManager mainMenuManager;

    protected override void Awake()
    {
        base.Awake();

        buttonPlay.onClick.AddListener(OnButtonPlayClicked);
        buttonSettings.onClick.AddListener(OnButtonSettingsClicked);
        buttonCredits.onClick.AddListener(OnButtonCreditsClicked);
        buttonExit.onClick.AddListener(OnButtonExitClicked);
    }

    private void OnDestroy()
    {
        buttonPlay.onClick.RemoveAllListeners();
        buttonSettings.onClick.RemoveAllListeners();
        buttonCredits.onClick.RemoveAllListeners();
        buttonExit.onClick.RemoveAllListeners();
    }

    private void OnButtonPlayClicked()
    {
        SceneManager.LoadScene("SCN_Gameplay");
    }

    private void OnButtonSettingsClicked()
    {
        mainMenuManager.ShowSettingsPanel();
    }

    private void OnButtonCreditsClicked()
    {
        mainMenuManager.ShowCreditsPanel();
    }

    private void OnButtonExitClicked()
    {
        mainMenuManager.ShowExitPopup();
    }
}