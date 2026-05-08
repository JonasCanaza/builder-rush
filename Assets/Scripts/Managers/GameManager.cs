public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public int Score { get; private set; }
    public int PerfectPlacements { get; private set; }
    public int BlocksPlaced { get; private set; }

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
    }
}