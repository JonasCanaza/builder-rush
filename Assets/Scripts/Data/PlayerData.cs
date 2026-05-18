using UnityEngine;

public static class PlayerData
{
    public const string KEY_BEST_SCORE = "BestScore";

    public static int BestScore
    {
        get
        {
            return PlayerPrefs.GetInt(KEY_BEST_SCORE, 0);
        }
        set
        {
            PlayerPrefs.SetInt(KEY_BEST_SCORE, value);
            PlayerPrefs.Save();
        }
    }
}