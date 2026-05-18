using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class GameOverPopup : UIPopup
{
    public event Action OnRestartPressed;
    public event Action OnExitPressed;

    [Header("Text Reference")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text bestText;

    [Header("Button Reference")]
    [SerializeField] private Button restartButton;
    [SerializeField] private Button exitButton;

    [Header("Clip Reference")]
    [SerializeField] private AudioClip loserClip;

    protected override void Awake()
    {
        base.Awake();

        restartButton.onClick.AddListener(HandleRestartClicked);
        exitButton.onClick.AddListener(HandleExitClicked);
    }

    private void OnDestroy()
    {
        restartButton.onClick.RemoveListener(HandleRestartClicked);
        exitButton.onClick.RemoveListener(HandleExitClicked);
    }

    private void HandleRestartClicked()
    {
        OnRestartPressed?.Invoke();
    }

    private void HandleExitClicked()
    {
        OnExitPressed?.Invoke();
    }

    public void ShowResults()
    {
        AudioManager.Instance.PlaySFX(loserClip);

        scoreText.text = $"Score: {GameManager.Instance.Score}";
        bestText.text = $"Best: {PlayerData.BestScore}";
    }
}