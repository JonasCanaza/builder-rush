using UnityEngine;

public class TowerDropperController : MonoBehaviour
{
    [Header("Tower Settings")]
    [SerializeField] private TowerController towerPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform towerContainer;
    private TowerController currentTower;

    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 6.0f;
    [SerializeField] private float movementLimitX = 6.0f;
    private float direction = 1.0f;

    [Header("Clips Settings")]
    [SerializeField] private AudioClip launchSfx;

    [Header("Pause Settings")]
    [SerializeField] private GameplayUIManager gameplayUIManager;

    private void Start()
    {
        SpawnTower();

        GameManager.Instance.OnTowerPlaced += SpawnTower;
    }

    private void Update()
    {
        ReadPauseInput();

        if (!gameplayUIManager.IsPaused)
        {
            ReadGameplayInput();
            Movement();

            FollowCurrentTower();
        }
    }

    private void OnDestroy()
    {
        if (GameManager.Instance)
        {
            GameManager.Instance.OnTowerPlaced -= SpawnTower;
        }
    }

    private void ReadPauseInput()
    {
        // PAUSE
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            gameplayUIManager.ToggleShowPausePanel();
        }
    }

    private void ReadGameplayInput()
    {
        // DROP
        if (Input.GetKeyDown(KeyCode.Space) && currentTower)
        {
            ReleaseCurrentTower();
        }
    }

    private void Movement()
    {
        transform.position += Vector3.right * (direction * movementSpeed * Time.deltaTime);

        if (Mathf.Abs(transform.position.x) >= movementLimitX)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = Mathf.Sign(newPosition.x) * movementLimitX;
            transform.position = newPosition;

            direction *= -1;
        }
    }

    private void FollowCurrentTower()
    {
        if (currentTower)
        {
            currentTower.transform.position = spawnPoint.position;
        }
    }

    private void SpawnTower()
    {
        currentTower = Instantiate(towerPrefab, spawnPoint.position, Quaternion.identity, towerContainer);
        currentTower.AttachToDropper();
    }

    private void ReleaseCurrentTower()
    {
        currentTower.Release();
        currentTower = null;

        AudioManager.Instance.PlaySFX(launchSfx);
    }
}