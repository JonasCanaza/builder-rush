using UnityEngine;
using UnityEngine.UI;
using System;

public class ExitLevelPopup : UIPopup
{
    public event Action OnExitConfirmed;

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
        OnExitConfirmed?.Invoke();
    }
}