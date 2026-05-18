using UnityEngine;
using TMPro;

public class GameplayPanelController : UIPanel
{
    [Header("Text References")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text perfectPlacementsText;
    [SerializeField] private TMP_Text towersPlacedText;

    private void Start()
    {
        GameManager.Instance.OnScoreChanged += UpdateScoreText;
        GameManager.Instance.OnPerfectPlacementsChanged += UpdatePerfectPlacementsText;
        GameManager.Instance.OnTowersPlacedChanged += UpdateTowersPlacedText;

        UpdateScoreText(GameManager.Instance.Score);
        UpdatePerfectPlacementsText(GameManager.Instance.PerfectStreak);
        UpdateTowersPlacedText(GameManager.Instance.TowersPlaced);
    }

    private void OnDestroy()
    {
        if (GameManager.Instance)
        {
            GameManager.Instance.OnScoreChanged -= UpdateScoreText;
            GameManager.Instance.OnPerfectPlacementsChanged -= UpdatePerfectPlacementsText;
            GameManager.Instance.OnTowersPlacedChanged -= UpdateTowersPlacedText;
        }
    }

    private void UpdateScoreText(int amount)
    {
        scoreText.text = $"Score: {amount}";
    }

    private void UpdatePerfectPlacementsText(int amount)
    {
        perfectPlacementsText.text = $"Perfects: {amount}";
    }

    private void UpdateTowersPlacedText(int amount)
    {
        towersPlacedText.text = $"Towers: {amount}";
    }
}