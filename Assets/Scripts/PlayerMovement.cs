using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f; // Kecepatan pemain
    public float dodgeSpeed = 15f;
    public float dodgeDuration = 0.2f;
    public float dodgeCooldown = 1f;
    private bool isDodging = false;
    private float dodgeTime;
    private float lastDodgeTime;
    public float groundDist;
    public float sensitivity = 100f; // Sensitivitas gerakan kamera
    public float slopeLimit = 60f; // Kemiringan maksimum yang bisa didaki

    public LayerMask terrainLayer;
    public Rigidbody rb;
    public SpriteRenderer sr;
    private Animator anim;
    [SerializeField] Transform cameraTransform;
    private float x, y;
    private Vector3 moveDir;
    private enum MovementState { idle, running }
    private BossActivation bossActivation;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Menangani interaksi dengan terrain
        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1;
        if (Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer))
        {
            if (hit.collider != null)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDist;
                transform.position = movePos;
            }
        }

        // Input pergerakan pemain
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        UpdateAnimationState();

        // Check for dodge input
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= lastDodgeTime + dodgeCooldown)
        {
            StartDodge();
        }

        // Arah pergerakan pemain mengikuti arah kamera, dengan mempertimbangkan kemiringan
        moveDir = GetSlopeAdjustedMoveDirection(cameraTransform.right * x + cameraTransform.forward * y);
        moveDir.y = 0; // Pastikan pemain tidak bergerak di sumbu Y
        moveDir.Normalize();

        // Terapkan kecepatan pada Rigidbody
        if (!isDodging)
        {
            rb.velocity = moveDir * speed;
        }

        // Flipping sprite berdasarkan arah horizontal
        if (x != 0 && x < 0)
        {
            sr.flipX = true;
        }
        else if (x != 0 && x > 0)
        {
            sr.flipX = false;
        }

        // Kontrol kamera dengan pembalikan arah horizontal
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        // Membalik arah rotasi horizontal (kiri-kanan)
        mouseX = -mouseX;

        // Rotasi horizontal pemain mengikuti input mouse yang sudah dibalik
        transform.Rotate(Vector3.up * mouseX);
    }

    private void FixedUpdate()
{
    if (isDodging)
    {
        Dodge();
    }
    else
    {
        // Terapkan gaya dorong untuk membantu mendaki
        Vector3 force = moveDir * speed * Time.fixedDeltaTime;
        rb.AddForce(force, ForceMode.VelocityChange);
    }
}

    private void UpdateAnimationState()
    {
        MovementState state;

        if (x > 0f || x < 0f || y > 0f)
        {
            state = MovementState.running;
        }
        else
        {
            state = MovementState.idle;
        }
        anim.SetInteger("state", (int)state);
    }

    private void StartDodge()
    {
        isDodging = true;
        dodgeTime = Time.time;
        lastDodgeTime = Time.time;
    }

    void Dodge()
    {
        // Move player faster in the direction they are walking
        rb.MovePosition(rb.position + moveDir * dodgeSpeed * Time.fixedDeltaTime);

        // End dodge after duration
        if (Time.time >= dodgeTime + dodgeDuration)
        {
            isDodging = false;
        }
    }

    // Fungsi untuk menyesuaikan arah gerakan sesuai dengan kemiringan
    private Vector3 GetSlopeAdjustedMoveDirection(Vector3 inputDirection)
{
    RaycastHit hit;
    if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f, terrainLayer))
    {
        Vector3 slopeNormal = hit.normal;
        Vector3 slopeForward = Vector3.Cross(slopeNormal, transform.right).normalized;
        // Debug.Log("Slope Normal: " + slopeNormal);
        // Debug.Log("Slope Forward: " + slopeForward);
        return Vector3.ProjectOnPlane(inputDirection, slopeNormal).normalized * speed;
    }
    return inputDirection;
}

    private void OnDrawGizmos()
{
    if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1f, terrainLayer))
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, hit.point);
        Gizmos.DrawRay(hit.point, hit.normal);
    }
}
}
