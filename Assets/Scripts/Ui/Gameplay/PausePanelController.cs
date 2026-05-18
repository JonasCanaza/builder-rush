using UnityEngine;
using UnityEngine.UI;
using System;

public class PausePanelController : UIPanel
{
    public event Action OnResumePressed;
    public event Action OnRestartPressed;
    public event Action OnSettingsPressed;
    public event Action OnExitPressed;

    [Header("Button References")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;

    protected override void Awake()
    {
        base.Awake();

        resumeButton.onClick.AddListener(HandleResumeClicked);
        restartButton.onClick.AddListener(HandleRestartClicked);
        settingsButton.onClick.AddListener(HandleSettingsClicked);
        exitButton.onClick.AddListener(HandleExitClicked);
    }

    private void OnDestroy()
    {
        resumeButton.onClick.RemoveListener(HandleResumeClicked);
        restartButton.onClick.RemoveListener(HandleRestartClicked);
        settingsButton.onClick.RemoveListener(HandleSettingsClicked);
        exitButton.onClick.RemoveListener(HandleExitClicked);
    }

    private void HandleResumeClicked()
    {
        OnResumePressed?.Invoke();
    }

    private void HandleRestartClicked()
    {
        OnRestartPressed?.Invoke();
    }

    private void HandleSettingsClicked()
    {
        OnSettingsPressed?.Invoke();
    }

    private void HandleExitClicked()
    {
        OnExitPressed?.Invoke();
    }
}