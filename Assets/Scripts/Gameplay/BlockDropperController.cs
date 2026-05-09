using UnityEngine;

public class BlockDropperController : MonoBehaviour
{
    [Header("Block Settings")]
    [SerializeField] private GameObject blockPrefab;

    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 6.0f;
    [SerializeField] private float movementLimitX = 6.0f;
    private float direction = 1.0f;

    [Header("Clips Settings")]
    [SerializeField] private AudioClip launchSfx;

    private void Update()
    {
        ReadInput();
        Movement();
    }

    private void ReadInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
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
}