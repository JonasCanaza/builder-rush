using UnityEngine;
using UnityEngine.UI;
using System;

public class RestartLevelPopup : UIPopup
{
    public event Action OnRestartPressed;

    [Header("Button References")]
    [SerializeField] private Button restartButton;
    [SerializeField] private Button cancelButton;

    protected override void Awake()
    {
        base.Awake();

        restartButton.onClick.AddListener(HandleRestartClicked);
        cancelButton.onClick.AddListener(HandleCancelClicked);
    }

    private void OnDestroy()
    {
        restartButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();
    }

    private void HandleRestartClicked()
    {
        OnRestartPressed?.Invoke();
    }

    private void HandleCancelClicked()
    {
        Hide();
    }
}