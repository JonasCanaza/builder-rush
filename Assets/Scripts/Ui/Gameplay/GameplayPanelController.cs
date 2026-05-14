using UnityEngine;
using TMPro;

public class GameplayPanelController : MonoBehaviour
{
    [Header("Panel Settings")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text perfectPlacementsText;
    [SerializeField] private TMP_Text towersPlacedsText;

    private void Start()
    {
        GameManager.Instance.OnScoreChanged += UpdateScore;
        GameManager.Instance.OnPerfectPlacementsChanged += UpdatePerfectPlacements;
        GameManager.Instance.OnTowersPlacedChanged += UpdateTowersPlaced;

        UpdateScore(GameManager.Instance.Score);
        UpdatePerfectPlacements(GameManager.Instance.PerfectStreak);
        UpdateTowersPlaced(GameManager.Instance.TowersPlaced);
    }

    private void OnDestroy()
    {
        if (GameManager.Instance)
        {
            GameManager.Instance.OnScoreChanged -= UpdateScore;
            GameManager.Instance.OnPerfectPlacementsChanged -= UpdatePerfectPlacements;
            GameManager.Instance.OnTowersPlacedChanged -= UpdateTowersPlaced;
        }
    }

    private void UpdateScore(int amount)
    {
        scoreText.text = $"Score: {amount}";
    }

    private void UpdatePerfectPlacements(int amount)
    {
        perfectPlacementsText.text = $"Perfects: {amount}";
    }

    private void UpdateTowersPlaced(int amount)
    {
        towersPlacedsText.text = $"Towers: {amount}";
    }
}