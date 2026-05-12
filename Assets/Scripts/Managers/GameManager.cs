using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    private const string GAMEPLAY_SCENE = "SCN_Gameplay";

    public event Action<int> OnScoreChanged;
    public event Action<int> OnPerfectPlacementsChanged;
    public event Action<int> OnTowersPlacedChanged;
    public event Action OnTowerPlaced;

    public bool IsGameOver { get; private set; }
    public int Score { get; private set; }
    public int PerfectPlacements { get; private set; }
    public int TowersPlaced { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

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

    public void RegisterTower()
    {
        TowersPlaced++;
        OnTowersPlacedChanged?.Invoke(TowersPlaced);
        OnTowerPlaced?.Invoke();
    }

    public void GameOver()
    {
        if (!IsGameOver)
        {
            IsGameOver = true;
            SceneManager.LoadScene(GAMEPLAY_SCENE);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetScore();
    }

    private void ResetScore()
    {
        IsGameOver = false;

        Score = 0;
        PerfectPlacements = 0;
        TowersPlaced = 0;

        OnScoreChanged?.Invoke(Score);
        OnPerfectPlacementsChanged?.Invoke(PerfectPlacements);
        OnTowersPlacedChanged?.Invoke(TowersPlaced);
    }
}