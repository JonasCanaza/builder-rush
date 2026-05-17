using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]

public class TowerController : MonoBehaviour
{
    [Header("Data Settings")]
    [SerializeField] private TowerSetupDataSO setupData;
    [SerializeField] private TowerReferencesSO referencesData;

    private const float GRAVITY_MULTIPLIER_OFFSET = 1.0f;
    private const float MAX_PERCENTAGE = 100.0f;

    private Rigidbody rb;
    private Collider col;
    private bool isPlaced = false;
    private bool attachedToDropper = true;

    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!isPlaced && !attachedToDropper)
        {
            ApplyExtraGravity();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isPlaced && !GameManager.Instance.IsGameOver)
        {
            PlaceTower(collision);
            AudioManager.Instance.PlaySFX(referencesData.CollisionSfx);

            if (GameManager.Instance.TowersPlaced == 0)
            {
                GameManager.Instance.AddScore(setupData.ScoreValue);
                GameManager.Instance.RegisterTower();
                AudioManager.Instance.PlaySFX(referencesData.GoodPlacedSfx);
            }
            else
            {
                EvaluatePlacement(collision);
            }
        }
    }

    public void AttachToDropper()
    {
        attachedToDropper = true;
        rb.isKinematic = true;
    }

    public void Release()
    {
        attachedToDropper = false;
        rb.isKinematic = false;
        animator.SetTrigger("TowerLaunch");
    }

    private void ApplyExtraGravity()
    {
        rb.AddForce(Physics.gravity * (setupData.GravityMultiplier - GRAVITY_MULTIPLIER_OFFSET), ForceMode.Acceleration);
    }

    private void PlaceTower(Collision collision)
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

        if (overlapPercentage > setupData.PerfectOverlap)
        {
            GameManager.Instance.AddScore(setupData.ScoreValue * 2);
            GameManager.Instance.AddPerfectPlacement();
            AudioManager.Instance.PlaySFX(referencesData.PerfectPlacedSfx);
        }
        else if (overlapPercentage > setupData.GoodOverlap)
        {
            GameManager.Instance.AddScore(setupData.ScoreValue);
            AudioManager.Instance.PlaySFX(referencesData.GoodPlacedSfx);
            GameManager.Instance.BreakStreak();
        }
        else
        {
            GameManager.Instance.BreakStreak();
            rb.constraints = RigidbodyConstraints.FreezePositionZ;
            rb.isKinematic = false;
            isValidPlacement = false;
        }

        if (isValidPlacement)
        {
            GameManager.Instance.RegisterTower();
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

        float towerWidth = col.bounds.size.x;

        return Mathf.Clamp((overlap / towerWidth) * MAX_PERCENTAGE, 0.0f, MAX_PERCENTAGE);
    }
}