using UnityEngine;
using UnityEngine.UI;

public class ExitGamePopup : UIPopup
{
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button exitButton;

    protected override void Awake()
    {
        base.Awake();

        cancelButton.onClick.AddListener(OnCancelButton);
        exitButton.onClick.AddListener(OnExitButton);
    }

    private void OnDestroy()
    {
        cancelButton.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();
    }

    private void OnCancelButton()
    {
        Hide();
    }

    private void OnExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}