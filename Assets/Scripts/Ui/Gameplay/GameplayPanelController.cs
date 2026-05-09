using UnityEngine;
using TMPro;

public class GameplayPanelController : MonoBehaviour
{
    [Header("Panel Settings")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text perfectPlacementsText;
    [SerializeField] private TMP_Text blocksPlacedText;

    private void Start()
    {
        GameManager.Instance.OnScoreChanged += UpdateScore;
        GameManager.Instance.OnPerfectPlacementsChanged += UpdatePerfectPlacements;
        GameManager.Instance.OnBlocksPlacedChanged += UpdateBlocksPlaced;

        UpdateScore(GameManager.Instance.Score);
        UpdatePerfectPlacements(GameManager.Instance.PerfectPlacements);
        UpdateBlocksPlaced(GameManager.Instance.BlocksPlaced);
    }

    private void OnDestroy()
    {
        if (GameManager.Instance)
        {
            GameManager.Instance.OnScoreChanged -= UpdateScore;
            GameManager.Instance.OnPerfectPlacementsChanged -= UpdatePerfectPlacements;
            GameManager.Instance.OnBlocksPlacedChanged -= UpdateBlocksPlaced;
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

    private void UpdateBlocksPlaced(int amount)
    {
        blocksPlacedText.text = $"Blocks: {amount}";
    }
}