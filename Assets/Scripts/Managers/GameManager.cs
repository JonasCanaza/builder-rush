using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    private const int FIRST_BLOCK = 1;
    private const string GAMEPLAY_SCENE = "SCN_Gameplay";

    public int Score { get; private set; }
    public int PerfectPlacements { get; private set; }
    public int BlocksPlaced { get; private set; }

    public event Action OnFirstBlockPlaced;

    public void AddScore(int points)
    {
        Score += points;
    }

    public void AddPerfectPlacement()
    {
        PerfectPlacements++;
    }

    public void RegisterBlock()
    {
        BlocksPlaced++;

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