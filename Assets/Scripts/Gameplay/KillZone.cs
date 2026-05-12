using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class KillZone : MonoBehaviour
{
    private Collider col;
    private int towerLayer;

    private void Awake()
    {
        col = GetComponent<Collider>();
        col.enabled = false;

        towerLayer = LayerMask.NameToLayer("Tower");
    }

    private void Start()
    {
        GameManager.Instance.OnTowersPlacedChanged += HandleTowersPlacedChanged;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnTowersPlacedChanged -= HandleTowersPlacedChanged;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == towerLayer && !GameManager.Instance.IsGameOver)
        {
            Rigidbody otherRb = other.GetComponent<Rigidbody>();

            if (otherRb != null && !otherRb.isKinematic)
            {
                GameManager.Instance.GameOver();
            }
        }
    }

    private void HandleTowersPlacedChanged(int towersPlaced)
    {
        col.enabled = towersPlaced > 0;
    }
}