using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainPanelController : MonoBehaviour
{
    [Header("References Settings")]
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonExit;

    private void Awake()
    {
        buttonPlay.onClick.AddListener(OnButtonPlayClicked);
        buttonExit.onClick.AddListener(OnButtonExitClicked);
    }

    private void OnDestroy()
    {
        buttonPlay.onClick.RemoveAllListeners();
        buttonExit.onClick.RemoveAllListeners();
    }

    private void OnButtonPlayClicked()
    {
        SceneManager.LoadScene("SCN_Gameplay");
    }

    private void OnButtonExitClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}