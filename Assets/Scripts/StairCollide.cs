using UnityEngine;
using UnityEngine.SceneManagement;

public class StairCollide : MonoBehaviour
{
    [SerializeField] private Transform teleportTarget; // The target location where the player will be teleported
    [SerializeField] private int minimumLevelRequirement;
    private bool isPlayerNear = false;
    private GameObject playerClone;

    void Start()
    {
        // Assuming the player clone is already present in the scene
        playerClone = GameObject.FindGameObjectWithTag("Player");
        if (playerClone == null)
        {
            Debug.LogError("Player clone not found in the scene!");
        }
    }

    void Update()
    {
        if (isPlayerNear)
        {
            if (GameManager.GetCurrentLevel() < minimumLevelRequirement) return;

            // Teleport the player clone to the target location
            TeleportPlayer();
        }
    }

    private void TeleportPlayer()
    {
        if (playerClone != null && teleportTarget != null)
        {
            playerClone.transform.position = teleportTarget.position;
            Debug.Log("Player teleported to: " + teleportTarget.position);
        }
        else
        {
            Debug.LogWarning("Teleportation failed. Player or teleport target not found.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log("Player left the teleportation area.");
        }
    }
}
