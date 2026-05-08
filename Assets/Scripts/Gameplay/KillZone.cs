using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class KillZone : MonoBehaviour
{
    private Collider col;
    private int blockLayer;

    private void Awake()
    {
        col = GetComponent<Collider>();
        col.enabled = false;

        blockLayer = LayerMask.NameToLayer("Block");
    }

    private void Start()
    {
        GameManager.Instance.OnFirstBlockPlaced += ActivateCollider;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnFirstBlockPlaced -= ActivateCollider;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == blockLayer)
        {
            Rigidbody otherRb = other.GetComponent<Rigidbody>();

            if (otherRb != null && !otherRb.isKinematic)
            {
                GameManager.Instance.GameOver();
            }
        }
    }

    private void ActivateCollider()
    {
        col.enabled = true;
    }
}