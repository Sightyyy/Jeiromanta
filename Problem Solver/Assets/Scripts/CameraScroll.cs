using UnityEngine;
using Cinemachine;

public class CameraScroll : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float scrollSpeed = 0.5f; // Speed at which the camera moves

    private Vector3 lastMousePosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Record the position of the mouse when the left button is pressed
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            // Calculate the difference between the current and last mouse position
            Vector3 delta = Input.mousePosition - lastMousePosition;

            // Adjust the camera position by this delta, multiplied by scroll speed
            Vector3 newPosition = virtualCamera.transform.position;
            newPosition -= new Vector3(delta.x * scrollSpeed * Time.deltaTime, delta.y * scrollSpeed * Time.deltaTime, 0);

            virtualCamera.transform.position = newPosition;

            // Update the last mouse position to the current one for the next frame
            lastMousePosition = Input.mousePosition;
        }
    }
}
