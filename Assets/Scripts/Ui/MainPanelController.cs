using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainPanelController : MonoBehaviour
{
    [Header("References Settings")]
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonSettings;
    [SerializeField] private Button buttonExit;
    [SerializeField] private MainMenuManager mainMenuManager;

    private void Awake()
    {
        buttonPlay.onClick.AddListener(OnButtonPlayClicked);
        buttonSettings.onClick.AddListener(OnButtonSettingsClicked);
        buttonExit.onClick.AddListener(OnButtonExitClicked);
    }

    private void OnDestroy()
    {
        buttonPlay.onClick.RemoveAllListeners();
        buttonSettings.onClick.RemoveAllListeners();
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

    private void OnButtonExitClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}