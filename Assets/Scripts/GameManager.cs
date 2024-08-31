using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject playerPrefab;
    private static GameObject currentPlayer;
    private static int currentLevel = 1;
    
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public static int GetCurrentLevel()
    {
        return currentLevel;
    }

    public static void AddLevel()
    {
        currentLevel++;
    }

    public static GameObject GetCurrentPlayer()
    {
        return currentPlayer;
    }

    private void Awake()
    {
        // Ensure that there's only one instance of GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Subscribe to the sceneLoaded event
        SceneManager.activeSceneChanged += OnSceneLoaded;
    }

    // Method to change the scene
    public void ChangeScene(string sceneName)
    {
        if (currentPlayer != null)
        {
            Destroy(currentPlayer);
        }
        SceneManager.LoadScene(sceneName);
    }

    // Called every time a new scene is loaded
    private void OnSceneLoaded(Scene current, Scene next)
    {
        string currentScene = current.name;
        if (currentScene == null)
        {
            currentScene = "MainScene";
        }
        Debug.LogFormat("Current: {0}, Next: {1}", current.name, next.name);
        if(currentScene == "MainScene" && next.name == "House")
        {
            currentPlayer = Instantiate(playerPrefab, new Vector3 (0, 1, -20), playerPrefab.transform.rotation);
        }
        // else if(currentScene == "MainScene" && next.name == "Dormitory")
        // {
        //     currentPlayer = Instantiate(playerPrefab, new Vector3 (0, 1, -20), playerPrefab.transform.rotation);
        // }
        else if(currentScene == "House" && next.name == "MainScene")
        {
            currentPlayer = Instantiate(playerPrefab, new Vector3 (-25, 1, -25), playerPrefab.transform.rotation);
        }
        else if(currentScene == "Dormitory" && next.name == "MainScene")
        {
            currentPlayer = Instantiate(playerPrefab, new Vector3 (10, 1, -42), playerPrefab.transform.rotation);
        }
    }
}