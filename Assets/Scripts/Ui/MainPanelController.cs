using UnityEngine;
using UnityEngine.UI;
using System;

public class MainPanelController : UIPanel
{
    public event Action OnPlayPressed;
    public event Action OnSettingsPressed;
    public event Action OnCreditsPressed;
    public event Action OnExitPressed;

    [Header("Button References")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;

    protected override void Awake()
    {
        base.Awake();

        playButton.onClick.AddListener(HandlePlayClicked);
        settingsButton.onClick.AddListener(HandleSettingsClicked);
        creditsButton.onClick.AddListener(HandleCreditsClicked);
        exitButton.onClick.AddListener(HandleExitClicked);
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveListener(HandlePlayClicked);
        settingsButton.onClick.RemoveListener(HandleSettingsClicked);
        creditsButton.onClick.RemoveListener(HandleCreditsClicked);
        exitButton.onClick.RemoveListener(HandleExitClicked);
    }

    private void HandlePlayClicked()
    {
        OnPlayPressed?.Invoke();
    }

    private void HandleSettingsClicked()
    {
        OnSettingsPressed?.Invoke();
    }

    private void HandleCreditsClicked()
    {
        OnCreditsPressed?.Invoke();
    }

    private void HandleExitClicked()
    {
        OnExitPressed?.Invoke();
    }
}