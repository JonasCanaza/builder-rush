using UnityEngine;
using UnityEngine.UI;
using System;

public class CreditsPanelController : UIPanel
{
    public event Action OnBackPressed;

    [Header("Scroll View Reference")]
    [SerializeField] private Scrollbar scrollBar;

    [Header("Button Reference")]
    [SerializeField] private Button backButton;

    protected override void Awake()
    {
        base.Awake();

        backButton.onClick.AddListener(HandleBackClicked);
    }

    private void OnDestroy()
    {
        backButton.onClick.RemoveListener(HandleBackClicked);
    }

    private void HandleBackClicked()
    {
        scrollBar.value = 1.0f;
        OnBackPressed?.Invoke();
    }
}