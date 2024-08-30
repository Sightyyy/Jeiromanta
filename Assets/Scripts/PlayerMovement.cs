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

    public LayerMask terrainLayer;
    public Rigidbody rb;
    public SpriteRenderer sr;
    private Animator anim;
    [SerializeField] Transform cameraTransform;
    private float x, y;
    private Vector3 moveDir;
    private enum MovementState { idle, running }
    private BossActivation bossActivation;
    private bool isCanDodge;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        // bossActivation = GameObject.Find("BossStart").GetComponent<BossActivation>();
    }

    void Update()
    {
        // isCanDodge = bossActivation.isBossStart;
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
        // Check for dodge input
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= lastDodgeTime + dodgeCooldown && isCanDodge)
        {
            StartDodge();
        }
        UpdateAnimationState();

        // Arah pergerakan pemain mengikuti arah kamera
        moveDir = cameraTransform.right * x + cameraTransform.forward * y;
        moveDir.y = 0; // Pastikan pemain tidak bergerak di sumbu Y
        moveDir.Normalize();
        rb.velocity = moveDir * speed;

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
    }
    private void UpdateAnimationState()
    {
        MovementState state;

        if (x > 0f || x < 0f || y > 0f || y < 0f)
        {
            state = MovementState.running;
            // if (soundManager.sfxSource.clip != soundManager.movementSound || !soundManager.sfxSource.isPlaying)
            // {
            //     soundManager.PlaySFXLoop(soundManager.movementSound);
            // }
        }
        else
        {
            state = MovementState.idle;
            // if (soundManager.sfxSource.clip == soundManager.movementSound)
            // {
            //     soundManager.StopSFXLoop();
            // }
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

    // private void OnApplicationFocus(bool focus)
    // {
    //     if (focus)
    //     {
    //         Cursor.lockState = CursorLockMode.Locked;
    //     }
    //     else
    //     {
    //         Cursor.lockState = CursorLockMode.None;
    //     }
    // }
}
