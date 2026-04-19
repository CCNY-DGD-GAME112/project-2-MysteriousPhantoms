using UnityEngine;

public class BaseAI : MonoBehaviour
{
    protected Rigidbody rb;
    public float moveSpeed = 3f;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
            rb.isKinematic = true;
    }
    
    protected void MoveTowards(Vector3 target)
    {
        if (rb == null) return;

        Vector3 direction = target - rb.position;
        direction.y = 0;
        if (direction.magnitude > 0.01f)
        {
            rb.MoveRotation(Quaternion.LookRotation(direction));
            rb.MovePosition(rb.position + direction.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }
}