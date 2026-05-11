using UnityEngine;

public class GameplayRootController : MonoBehaviour
{
    [SerializeField] private int blocksBeforeMove = 2;
    [SerializeField] private float moveHeight = 2.0f;
    [SerializeField] private float moveSpeed = 5.0f;

    private Vector3 targetPosition;

    private void Start()
    {
        GameManager.Instance.OnBlocksPlacedChanged += HandleBlockPlaced;

        targetPosition = transform.position;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance)
        {
            GameManager.Instance.OnBlocksPlacedChanged -= HandleBlockPlaced;
        }
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    private void HandleBlockPlaced(int blocksPlaced)
    {
        if (blocksPlaced > blocksBeforeMove)
        {
            targetPosition.y += moveHeight;
        }
    }
}