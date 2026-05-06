using UnityEngine;
using UnityEngine.UI;

public class SettingsPanelController : MonoBehaviour
{
    [Header("References Settings")]
    [SerializeField] private Button backButton;
    [SerializeField] private MainMenuManager mainMenuManager;

    private void Awake()
    {
        backButton.onClick.AddListener(OnButtonBackClicked);
    }

    private void OnDestroy()
    {
        backButton.onClick.RemoveAllListeners();
    }

    private void OnButtonBackClicked()
    {
        mainMenuManager.ShowMainPanel();
    }
}