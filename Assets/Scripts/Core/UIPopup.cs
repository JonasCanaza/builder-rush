using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CanvasGroup))]

public abstract class UIPopup : MonoBehaviour
{
    private Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Show()
    {
        animator.SetTrigger("Show");
    }

    public void Hide()
    {
        animator.SetTrigger("Hide");
    }
}