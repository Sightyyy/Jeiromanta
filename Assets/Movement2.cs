using UnityEngine;

public class Movement2 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody rb;

    private Vector3 moveDirection;

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        moveDirection = new Vector3(moveX, 0f, moveZ).normalized;
    }

    void FixedUpdate()
    {
        if (moveDirection != Vector3.zero)
        {
            // Tentukan arah berdasarkan lereng
            Vector3 slopeMovement = Vector3.ProjectOnPlane(moveDirection, GetSlopeNormal()).normalized;
            rb.MovePosition(rb.position + slopeMovement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private Vector3 GetSlopeNormal()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f))
        {
            return hit.normal;
        }

        return Vector3.up;  // Jika tidak ada lereng, gunakan normal vector ke atas.
    }
}
