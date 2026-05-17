using UnityEngine;
using UnityEngine.UI;
using System;

public class RestartLevelPopup : UIPopup
{
    public event Action OnRestartConfirmed;

    [SerializeField] private Button restartButton;
    [SerializeField] private Button cancelButton;

    protected override void Awake()
    {
        base.Awake();

        restartButton.onClick.AddListener(OnRestartButton);
        cancelButton.onClick.AddListener(OnCancelButton);
    }

    private void OnDestroy()
    {
        restartButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();
    }

    private void OnRestartButton()
    {
        OnRestartConfirmed?.Invoke();
    }

    private void OnCancelButton()
    {
        Hide();
    }
}