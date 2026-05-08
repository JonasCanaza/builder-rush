using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]

public class BlockController : MonoBehaviour
{
    private const float GRAVITY_MULTIPLIER_OFFSET = 1.0f;
    private const float MAX_PERCENTAGE = 100.0f;

    [Header("Physics Settings")]
    [SerializeField] private float gravityMultiplier = 4.0f;
    private Rigidbody rb;
    private Collider col;
    private bool isPlaced = false;

    [Header("Block Settings")]
    [SerializeField] private int scoreValue = 20;
    [SerializeField] private float perfectOverlap = 90.0f;
    [SerializeField] private float goodOverlap = 45.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    private void FixedUpdate()
    {
        if (!isPlaced)
        {
            ApplyExtraGravity();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isPlaced)
        {
            PlaceBlock(collision);

            if (GameManager.Instance.BlocksPlaced == 0)
            {
                GameManager.Instance.AddScore(scoreValue);
                GameManager.Instance.RegisterBlock();
            }
            else
            {
                EvaluatePlacement(collision);
            }
        }
    }

    private void ApplyExtraGravity()
    {
        rb.AddForce(Physics.gravity * (gravityMultiplier - GRAVITY_MULTIPLIER_OFFSET), ForceMode.Acceleration);
    }

    private void PlaceBlock(Collision collision)
    {
        StopPhysics();
        AlignOnTop(collision);

        isPlaced = true;
    }

    private void StopPhysics()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
    }

    private void AlignOnTop(Collision collision)
    {
        float thisHeight = col.bounds.size.y;
        float otherTop = collision.collider.bounds.max.y;

        Vector3 newPosition = transform.position;
        newPosition.y = otherTop + thisHeight / 2.0f;

        transform.position = newPosition;
    }

    private void EvaluatePlacement(Collision collision)
    {
        float overlapPercentage = GetOverlapPercentage(collision);
        bool isValidPlacement = true;

        if (overlapPercentage > perfectOverlap)
        {
            GameManager.Instance.AddScore(scoreValue * 2);
            GameManager.Instance.AddPerfectPlacement();
        }
        else if (overlapPercentage > goodOverlap)
        {
            GameManager.Instance.AddScore(scoreValue);
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezePositionZ;
            rb.isKinematic = false;
            isValidPlacement = false;
        }

        if (isValidPlacement)
        {
            GameManager.Instance.RegisterBlock();
        }
    }

    private float GetOverlapPercentage(Collision collision)
    {
        float thisLeftEdge = col.bounds.min.x;
        float thisRightEdge = col.bounds.max.x;
        float otherLeftEdge = collision.collider.bounds.min.x;
        float otherRightEdge = collision.collider.bounds.max.x;

        float overlapMin = Mathf.Max(thisLeftEdge, otherLeftEdge);
        float overlapMax = Mathf.Min(thisRightEdge, otherRightEdge);
        float overlap = Mathf.Max(0.0f, overlapMax - overlapMin);

        float blockWidth = col.bounds.size.x;

        return Mathf.Clamp((overlap / blockWidth) * MAX_PERCENTAGE, 0.0f, MAX_PERCENTAGE);
    }
}