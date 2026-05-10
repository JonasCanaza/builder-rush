using UnityEngine;
using UnityEngine.UI;
using System;

public class PausePanelController : MonoBehaviour
{
    public event Action OnResume;
    public event Action OnExit;

    [Header("Button Settings")]
    [SerializeField] private Button buttonResume;
    [SerializeField] private Button buttonExit;

    private void Awake()
    {
        buttonResume.onClick.AddListener(OnButtonResumeClicked);
        buttonExit.onClick.AddListener(OnButtonExitClicked);
    }

    private void OnDestroy()
    {
        buttonResume.onClick.RemoveAllListeners();
        buttonExit.onClick.RemoveAllListeners();
    }

    private void OnButtonResumeClicked()
    {
        OnResume?.Invoke();
    }

    private void OnButtonExitClicked()
    {
        OnExit?.Invoke();
    }
}