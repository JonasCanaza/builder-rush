using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    private const string GAMEPLAY_SCENE = "SCN_Gameplay";

    public event Action OnGameOver;
    public event Action<int> OnScoreChanged;
    public event Action<int> OnPerfectPlacementsChanged;
    public event Action<int> OnTowersPlacedChanged;
    public event Action OnTowerPlaced;

    public bool IsGameOver { get; private set; }
    public int Score { get; private set; }
    public int PerfectStreak { get; private set; }
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

        if (Score > PlayerData.BestScore)
        {
            PlayerData.BestScore = Score;

#if UNITY_EDITOR
            Debug.Log($"Best score: {PlayerData.BestScore}");
#endif
        }
    }

    public void AddPerfectPlacement()
    {
        PerfectStreak++;
        OnPerfectPlacementsChanged?.Invoke(PerfectStreak);
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
            OnGameOver?.Invoke();
        }
    }

    public void BreakStreak()
    {
        PerfectStreak = 0;
        OnPerfectPlacementsChanged?.Invoke(PerfectStreak);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == GAMEPLAY_SCENE)
        {
            ResetScore();
        }
    }

    private void ResetScore()
    {
        IsGameOver = false;

        Score = 0;
        PerfectStreak = 0;
        TowersPlaced = 0;

        OnScoreChanged?.Invoke(Score);
        OnPerfectPlacementsChanged?.Invoke(PerfectStreak);
        OnTowersPlacedChanged?.Invoke(TowersPlaced);
    }
}