using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class SettingsPanelController : UIPanel
{
    public event Action OnBackPressed;

    [Header("Button Reference")]
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

        backButton.onClick.AddListener(HandleBackClicked);

        sliderMaster.onValueChanged.AddListener(HandleMasterVolumeChanged);
        sliderMusic.onValueChanged.AddListener(HandleMusicVolumeChanged);
        sliderSfx.onValueChanged.AddListener(HandleSfxVolumeChanged);
    }

    private void Start()
    {
        sliderMaster.value = AudioData.MasterVolume;
        sliderMusic.value = AudioData.MusicVolume;
        sliderSfx.value = AudioData.SfxVolume;
    }

    private void OnDestroy()
    {
        backButton.onClick.RemoveListener(HandleBackClicked);

        sliderMaster.onValueChanged.RemoveListener(HandleMasterVolumeChanged);
        sliderMusic.onValueChanged.RemoveListener(HandleMusicVolumeChanged);
        sliderSfx.onValueChanged.RemoveListener(HandleSfxVolumeChanged);
    }

    private void HandleBackClicked()
    {
        OnBackPressed?.Invoke();
    }

    private void HandleMasterVolumeChanged(float currentValue)
    {
        SetVolume(AudioData.KEY_MASTER_VOLUME, currentValue);
        AudioData.MasterVolume = currentValue;
    }

    private void HandleMusicVolumeChanged(float currentValue)
    {
        SetVolume(AudioData.KEY_MUSIC_VOLUME, currentValue);
        AudioData.MusicVolume = currentValue;
    }

    private void HandleSfxVolumeChanged(float currentValue)
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