using UnityEngine;

public class BlockController : MonoBehaviour
{
    [Header("Physics Settings")]
    [SerializeField] private float gravityMultiplier = 4.0f;
    private Rigidbody rb;
    private Collider col;
    private bool isPlaced = false;

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
        }
    }

    private void ApplyExtraGravity()
    {
        rb.AddForce(Physics.gravity * (gravityMultiplier - 1.0f), ForceMode.Acceleration);
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
        float height = col.bounds.size.y;
        float otherTop = collision.collider.bounds.max.y;

        Vector3 newPosition = transform.position;
        newPosition.y = otherTop + height / 2.0f;

        transform.position = newPosition;
    }
}