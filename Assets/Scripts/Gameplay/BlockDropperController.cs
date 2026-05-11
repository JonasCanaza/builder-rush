using UnityEngine;

public class BlockDropperController : MonoBehaviour
{
    [Header("Block Settings")]
    [SerializeField] private BlockController blockPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform blocksContainer;
    private BlockController currentBlock;

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
        SpawnBlock();

        GameManager.Instance.OnBlockPlaced += SpawnBlock;
    }

    private void Update()
    {
        ReadPauseInput();

        if (!gameplayUIManager.IsPaused)
        {
            ReadGameplayInput();
            Movement();

            FollowCurrentBlock();
        }
    }

    private void OnDestroy()
    {
        if (GameManager.Instance)
        {
            GameManager.Instance.OnBlockPlaced -= SpawnBlock;
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
        if (Input.GetKeyDown(KeyCode.Space) && currentBlock)
        {
            ReleaseCurrentBlock();
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

    private void FollowCurrentBlock()
    {
        if (currentBlock)
        {
            currentBlock.transform.position = spawnPoint.position;
        }
    }

    private void SpawnBlock()
    {
        currentBlock = Instantiate(blockPrefab, spawnPoint.position, Quaternion.identity, blocksContainer);
        currentBlock.AttachToDropper();
    }

    private void ReleaseCurrentBlock()
    {
        currentBlock.Release();
        currentBlock = null;

        AudioManager.Instance.PlaySFX(launchSfx);
    }
}