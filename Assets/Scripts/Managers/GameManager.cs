using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public int Score { get; private set; }

    public void AddScore(int points)
    {
        Score += points;
        Debug.Log("Puntos: " + Score);
    }
}