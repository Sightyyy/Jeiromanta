using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentPlayer : MonoBehaviour
{
    private static PersistentPlayer instance;

    private void Awake()
    {
        // Ensure that only one instance of the player exists across scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep this player object alive across scenes
        }
        // else
        // {
        //     Destroy(gameObject); // Destroy duplicates
        // }
    }

    public void SetPlayerPosition(Vector3 position)
    {
        transform.position = position;
    }
}
