using UnityEngine;

public static class PlayerData
{
    public const string KEY_SCORE = "Score";

    public static int Score
    {
        get
        {
            return PlayerPrefs.GetInt(KEY_SCORE, 0);
        }
        set
        {
            PlayerPrefs.SetInt(KEY_SCORE, value);
            PlayerPrefs.Save();
        }
    }
}