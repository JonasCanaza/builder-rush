using UnityEngine;
using UnityEngine.UI;
using System;

public class PausePanelController : UIPanel
{
    public event Action OnResumeButtonClicked;
    public event Action OnSettingsButtonClicked;
    public event Action OnExitButtonClicked;

    [Header("Button Settings")]
    [SerializeField] private Button buttonResume;
    [SerializeField] private Button buttonSettings;
    [SerializeField] private Button buttonExit;

    protected override void Awake()
    {
        base.Awake();

        buttonResume.onClick.AddListener(OnButtonResumeClicked);
        buttonSettings.onClick.AddListener(OnButtonSettingsClicked);
        buttonExit.onClick.AddListener(OnButtonExitClicked);
    }

    private void OnDestroy()
    {
        buttonResume.onClick.RemoveAllListeners();
        buttonSettings.onClick.RemoveAllListeners();
        buttonExit.onClick.RemoveAllListeners();
    }

    private void OnButtonResumeClicked()
    {
        OnResumeButtonClicked?.Invoke();
    }

    private void OnButtonSettingsClicked()
    {
        OnSettingsButtonClicked?.Invoke();
    }

    private void OnButtonExitClicked()
    {
        OnExitButtonClicked?.Invoke();
    }
}