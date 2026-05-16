using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;

public class SettingsPanelController : UIPanel
{
    public event Action OnBackPressed;

    [Header("Panel Settings")]
    [SerializeField] private Button backButton;

    [Header("Audio Settings")]
    [SerializeField] private Slider sliderMaster;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSfx;
    [SerializeField] private AudioMixer audioMixer;
    private const float MIN_VOLUME = 0.0001f;
    private const float MAX_VOLUME = 1.0f;
    private const float DECIBEL_MULTIPLIER = 20.0f;

    protected override void Awake()
    {
        base.Awake();

        backButton.onClick.AddListener(OnButtonBackClicked);

        sliderMaster.onValueChanged.AddListener(OnMasterVolumeChanged);
        sliderMusic.onValueChanged.AddListener(OnMusicVolumeChanged);
        sliderSfx.onValueChanged.AddListener(OnSfxVolumeChanged);
    }

    private void Start()
    {
        sliderMaster.value = AudioData.MasterVolume;
        sliderMusic.value = AudioData.MusicVolume;
        sliderSfx.value = AudioData.SfxVolume;
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
        SetVolume(AudioData.KEY_MASTER_VOLUME, currentValue);
        AudioData.MasterVolume = currentValue;
    }

    private void OnMusicVolumeChanged(float currentValue)
    {
        SetVolume(AudioData.KEY_MUSIC_VOLUME, currentValue);
        AudioData.MusicVolume = currentValue;
    }

    private void OnSfxVolumeChanged(float currentValue)
    {
        SetVolume(AudioData.KEY_SFX_VOLUME, currentValue);
        AudioData.SfxVolume = currentValue;
    }

    private void SetVolume(string key, float value)
    {
        float volume = Mathf.Clamp(value, MIN_VOLUME, MAX_VOLUME);
        float decibels = Mathf.Log10(volume) * DECIBEL_MULTIPLIER;

        audioMixer.SetFloat(key, decibels);
    }
}