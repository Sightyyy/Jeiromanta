using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f; // Kecepatan pemain
    public float groundDist;
    public float sensitivity = 100f; // Sensitivitas gerakan kamera

    public LayerMask terrainLayer;
    public Rigidbody rb;
    public SpriteRenderer sr;
    [SerializeField] Transform cameraTransform;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
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
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        // Arah pergerakan pemain mengikuti arah kamera
        Vector3 moveDir = cameraTransform.right * x + cameraTransform.forward * y;
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
