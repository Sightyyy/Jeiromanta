using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleportation : MonoBehaviour
{
    public string sceneToLoad; // The name of the scene to load
    // public Vector3 targetPositionInNewScene; // The position where the player should appear in the new scene
    [SerializeField] private int minimumLevelRequirement;
    private bool isPlayerNear = false;

    Lobby lobby;
    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            // Move to the new scene and position the player
            if (GameManager.GetCurrentLevel() < minimumLevelRequirement) return;
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("Player is near the door. Press E to enter.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log("Player left the door area.");
        }
    }

    // private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     // Find the player in the new scene and set its position
    //     PersistentPlayer player = FindObjectOfType<PersistentPlayer>();
    //     if (player != null)
    //     {
    //         player.SetPlayerPosition(targetPositionInNewScene);
    //     }

    //     // Unsubscribe from the event
    //     SceneManager.sceneLoaded -= OnSceneLoaded;
    // }
}
