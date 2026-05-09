using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviourSingleton<AudioManager>
{
    [Header("Scenes Settings")]
    public const string MainMenu = "SCN_MainMenu";

    [Header("Sources Settings")]
    [SerializeField] private AudioSource musicSource;

    [Header("Clips Settings")]
    [SerializeField] private AudioClip menuMusic;

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

    public void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip != clip)
        {
            musicSource.loop = true;
            musicSource.clip = clip;
            musicSource.Play();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case MainMenu:

                PlayMusic(menuMusic);

                break;
        }
    }
}