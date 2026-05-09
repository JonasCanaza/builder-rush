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
        GameManager.Instance.OnBlocksPlacedChanged += HandleBlocksPlacedChanged;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnBlocksPlacedChanged -= HandleBlocksPlacedChanged;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == blockLayer && !GameManager.Instance.IsGameOver)
        {
            Rigidbody otherRb = other.GetComponent<Rigidbody>();

            if (otherRb != null && !otherRb.isKinematic)
            {
                GameManager.Instance.GameOver();
            }
        }
    }

    private void HandleBlocksPlacedChanged(int blocksPlaced)
    {
        col.enabled = blocksPlaced > 0;
    }
}