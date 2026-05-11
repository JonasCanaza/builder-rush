using UnityEngine;

public class BlockDropperController : MonoBehaviour
{
    [Header("Block Settings")]
    [SerializeField] private BlockController blockPrefab;

    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 6.0f;
    [SerializeField] private float movementLimitX = 6.0f;
    private float direction = 1.0f;
    private bool throwable;

    [Header("Clips Settings")]
    [SerializeField] private AudioClip launchSfx;

    [Header("Pause Settings")]
    [SerializeField] private GameplayUIManager gameplayUIManager;

    private void Start()
    {
        GameManager.Instance.OnBlockPlaced += ActivateThrowable;

        throwable = true;
    }

    private void Update()
    {
        ReadPauseInput();

        if (!gameplayUIManager.IsPaused)
        {
            ReadGameplayInput();
            Movement();
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnBlockPlaced -= ActivateThrowable;
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
        if (Input.GetKeyDown(KeyCode.Space) && throwable)
        {
            throwable = false;
            Instantiate(blockPrefab, transform.position, Quaternion.identity);
            AudioManager.Instance.PlaySFX(launchSfx);
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

    private void ActivateThrowable()
    {
        throwable = true;
    }
}