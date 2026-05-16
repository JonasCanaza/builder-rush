using UnityEngine;
using UnityEngine.UI;

public class CreditsPanelController : UIPanel
{
    [Header("Scroll View Settings")]
    [SerializeField] private Scrollbar scrollBar;

    [Header("References Settings")]
    [SerializeField] private Button backButton;
    [SerializeField] private MainMenuManager mainMenuManager;

    protected override void Awake()
    {
        base.Awake();

        backButton.onClick.AddListener(OnButtonBackClicked);
    }

    private void OnDestroy()
    {
        backButton.onClick.RemoveAllListeners();
    }

    private void OnButtonBackClicked()
    {
        scrollBar.value = 1.0f;
        mainMenuManager.ShowMainPanel();
    }
}