using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Camera mainCamera;
    public Vector3 offset = new Vector3(0, 0, 1); // Atur offset agar dekat di depan kamera

    void Update()
    {
        if (mainCamera != null)
        {
            // Tempatkan Canvas tepat di depan kamera dengan jarak yang sesuai
            transform.position = mainCamera.transform.position + mainCamera.transform.forward * offset.z;
            transform.rotation = mainCamera.transform.rotation;
        }
    }
}
