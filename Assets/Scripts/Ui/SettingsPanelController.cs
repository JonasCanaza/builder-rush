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
    [SerializeField] private AudioMixer audioMixer;
    private const string MasterVolumeKey = "MasterVolume";
    private const string MusicVolumeKey = "MusicVolume";

    private void Awake()
    {
        backButton.onClick.AddListener(OnButtonBackClicked);

        sliderMaster.onValueChanged.AddListener(OnMasterVolumeChanged);
        sliderMusic.onValueChanged.AddListener(OnMusicVolumeChanged);
    }

    private void OnDestroy()
    {
        backButton.onClick.RemoveAllListeners();

        sliderMaster.onValueChanged.RemoveAllListeners();
        sliderMusic.onValueChanged.RemoveAllListeners();
    }

    private void OnButtonBackClicked()
    {
        mainMenuManager.ShowMainPanel();
    }

    private void OnMasterVolumeChanged(float currentValue)
    {
        audioMixer.SetFloat(MasterVolumeKey, currentValue);
    }

    private void OnMusicVolumeChanged(float currentValue)
    {
        audioMixer.SetFloat(MusicVolumeKey, currentValue);
    }
}