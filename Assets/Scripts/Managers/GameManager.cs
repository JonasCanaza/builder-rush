using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    private const int FIRST_BLOCK = 1;
    private const string GAMEPLAY_SCENE = "SCN_Gameplay";

    public event Action<int> OnScoreChanged;
    public event Action<int> OnPerfectPlacementsChanged;
    public event Action<int> OnBlocksPlacedChanged;
    public event Action OnFirstBlockPlaced;

    public int Score { get; private set; }
    public int PerfectPlacements { get; private set; }
    public int BlocksPlaced { get; private set; }

    public void AddScore(int points)
    {
        Score += points;
        OnScoreChanged?.Invoke(Score);
    }

    public void AddPerfectPlacement()
    {
        PerfectPlacements++;
        OnPerfectPlacementsChanged?.Invoke(PerfectPlacements);
    }

    public void RegisterBlock()
    {
        BlocksPlaced++;
        OnBlocksPlacedChanged?.Invoke(BlocksPlaced);

        if (BlocksPlaced == FIRST_BLOCK)
        {
            OnFirstBlockPlaced?.Invoke();
        }
    }

    public void GameOver()
    {
        ResetScore();
        SceneManager.LoadScene(GAMEPLAY_SCENE);
    }

    private void ResetScore()
    {
        Score = 0;
        PerfectPlacements = 0;
        BlocksPlaced = 0;
    }
}