using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;

public class SettingsPanelController : MonoBehaviour
{
    public event Action OnBackPressed;

    [Header("Panel Settings")]
    [SerializeField] private Button backButton;

    [Header("Audio Settings")]
    [SerializeField] private Slider sliderMaster;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSfx;
    [SerializeField] private AudioMixer audioMixer;
    private const string MASTER_VOLUME_KEY = "MasterVolume";
    private const string MUSIC_VOLUME_KEY = "MusicVolume";
    private const string SFX_VOLUME_KEY = "SfxVolume";
    private const float MIN_VOLUME = 0.0001f;
    private const float MAX_VOLUME = 1.0f;
    private const float DECIBEL_MULTIPLIER = 20.0f;

    private void Awake()
    {
        backButton.onClick.AddListener(OnButtonBackClicked);

        sliderMaster.onValueChanged.AddListener(OnMasterVolumeChanged);
        sliderMusic.onValueChanged.AddListener(OnMusicVolumeChanged);
        sliderSfx.onValueChanged.AddListener(OnSfxVolumeChanged);
    }

    private void OnDestroy()
    {
        backButton.onClick.RemoveAllListeners();

        sliderMaster.onValueChanged.RemoveAllListeners();
        sliderMusic.onValueChanged.RemoveAllListeners();
        sliderSfx.onValueChanged.RemoveAllListeners();
    }

    private void OnButtonBackClicked()
    {
        OnBackPressed?.Invoke();
    }

    private void OnMasterVolumeChanged(float currentValue)
    {
        SetVolume(MASTER_VOLUME_KEY, currentValue);
    }

    private void OnMusicVolumeChanged(float currentValue)
    {
        SetVolume(MUSIC_VOLUME_KEY, currentValue);
    }

    private void OnSfxVolumeChanged(float currentValue)
    {
        SetVolume(SFX_VOLUME_KEY, currentValue);
    }

    private void SetVolume(string key, float value)
    {
        float volume = Mathf.Clamp(value, MIN_VOLUME, MAX_VOLUME);
        float decibels = Mathf.Log10(volume) * DECIBEL_MULTIPLIER;

        audioMixer.SetFloat(key, decibels);
    }
}