using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]

public class UIButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    [Header("Audio Settings")]
    [SerializeField] private AudioClip clickClip;
    [SerializeField] private AudioClip hoverClip;

    private Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySFX(clickClip);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySFX(hoverClip);
    }
}