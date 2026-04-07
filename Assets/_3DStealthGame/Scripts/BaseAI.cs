using UnityEngine;

public class BaseAI : MonoBehaviour
{
    protected Rigidbody rb;
    public float moveSpeed = 3f;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
            rb.isKinematic = true; // we move manually using MovePosition
    }

    // Helper to move toward a position
    protected void MoveTowards(Vector3 target)
    {
        if (rb == null) return;

        Vector3 direction = target - rb.position;
        direction.y = 0; // optional: keep movement on XZ plane
        if (direction.magnitude > 0.01f)
        {
            rb.MoveRotation(Quaternion.LookRotation(direction));
            rb.MovePosition(rb.position + direction.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }
}