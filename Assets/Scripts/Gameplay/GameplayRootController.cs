using UnityEngine;

public class GameplayRootController : MonoBehaviour
{
    [SerializeField] private int towersBeforeMove = 2;
    [SerializeField] private float moveHeight = 2.0f;
    [SerializeField] private float moveSpeed = 5.0f;

    private Vector3 targetPosition;

    private void Start()
    {
        GameManager.Instance.OnTowersPlacedChanged += HandleTowerPlaced;

        targetPosition = transform.position;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance)
        {
            GameManager.Instance.OnTowersPlacedChanged -= HandleTowerPlaced;
        }
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    private void HandleTowerPlaced(int towersPlaced)
    {
        if (towersPlaced > towersBeforeMove)
        {
            targetPosition.y += moveHeight;
        }
    }
}