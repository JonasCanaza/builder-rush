using UnityEngine;
using UnityEngine.UI;
using System;

public class ExitGamePopup : UIPopup
{
    public event Action OnExitPressed;

    [Header("Button References")]
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button exitButton;

    protected override void Awake()
    {
        base.Awake();

        cancelButton.onClick.AddListener(HandleCancelClicked);
        exitButton.onClick.AddListener(HandleExitClicked);
    }

    private void OnDestroy()
    {
        cancelButton.onClick.AddListener(HandleCancelClicked);
        exitButton.onClick.AddListener(HandleExitClicked);
    }

    private void HandleCancelClicked()
    {
        Hide();
    }

    private void HandleExitClicked()
    {
        OnExitPressed?.Invoke();
    }
}