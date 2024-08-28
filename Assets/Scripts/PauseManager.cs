using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject mazeCanvas, pauseCanvas;
    public GameObject player, mazePlayer;

    public void Pause()
    {
        // Access the static MazePlayer property from MazeGenerator
        GameObject mazePlayer = MazeGenerator.MazePlayer;

        if (mazePlayer != null)
        {
            // Get the MazePlayerMovement component from the instantiated player
            MazePlayerMovement mazePlayerMovement = mazePlayer.GetComponent<MazePlayerMovement>();

            // Disable the movement script
            if (mazePlayerMovement != null)
            {
                mazePlayerMovement.enabled = false;
            }
        }
        // Activate the pause canvas
        pauseCanvas.SetActive(true);
    }

    public void Resume()
    {
        Debug.Log("Resume button is called");
        MazePlayerMovement mazePlayerMovement = mazePlayer.GetComponent<MazePlayerMovement>();
        if (mazePlayerMovement != null)
        {
            mazePlayerMovement.enabled = true;
        }
        pauseCanvas.SetActive(false);
    }

    public void Quit()
    {
        player = GameObject.Find("Player");
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }
        MazePlayerMovement mazePlayerMovement = mazePlayer.GetComponent<MazePlayerMovement>();
        if (mazePlayerMovement != null)
        {
            mazePlayerMovement.enabled = true;
        }
        MazeGenerator.DestroyMazePlayer();
        pauseCanvas.SetActive(false);
        mazeCanvas.SetActive(false);
    }

    // public void IngameResume()
    // {
    //     GameManager.instance.Resume();
    //     creditsCanvas.SetActive(false);
    //     optionsCanvas.SetActive(false);
    //     pauseCanvas.SetActive(false);
    //     pauseButton.SetActive(true);
    //     resumeButton.SetActive(false);
    //     Debug.Log("Resume Clicked");
    // }
    // public void IngamePause()
    // {
    //     GameManager.instance.Pause();
    //     pauseCanvas.SetActive(true);
    //     resumeButton.SetActive(true);
    //     pauseButton.SetActive(false);
    //     Debug.Log("Pause Clicked");
    // }
}
