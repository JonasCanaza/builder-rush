using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsPanelController : MonoBehaviour
{
    [Header("Panel Settings")]
    [SerializeField] private Button backButton;
    [SerializeField] private MainMenuManager mainMenuManager;

    [Header("Audio Settings")]
    [SerializeField] private Slider sliderMaster;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSfx;
    [SerializeField] private AudioMixer audioMixer;
    private const string MASTER_VOLUME_KEY = "MasterVolume";
    private const string MUSIC_VOLUME_KEY = "MusicVolume";
    private const string SFX_VOLUME_KEY = "SfxVolume";

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
        mainMenuManager.ShowMainPanel();
    }

    private void OnMasterVolumeChanged(float currentValue)
    {
        audioMixer.SetFloat(MASTER_VOLUME_KEY, currentValue);
    }

    private void OnMusicVolumeChanged(float currentValue)
    {
        audioMixer.SetFloat(MUSIC_VOLUME_KEY, currentValue);
    }

    private void OnSfxVolumeChanged(float currentValue)
    {
        audioMixer.SetFloat(SFX_VOLUME_KEY, currentValue);
    }
}