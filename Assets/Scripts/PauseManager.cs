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
        Debug.Log("Pause function called");
        MazePlayerMovement mazePlayerMovement = mazePlayer.GetComponent<MazePlayerMovement>();
        if (mazePlayerMovement != null)
        {
            mazePlayerMovement.enabled = false;
            Debug.Log("MazePlayerMovement found and disabled");
        }
        else
        {
            Debug.LogWarning("MazePlayerMovement not found on mazePlayer");
        }
        mazeCanvas.SetActive(false);
        pauseCanvas.SetActive(true);
        Debug.Log("Changed to Pause Menu");
    }

    public void Resume()
    {
        MazePlayerMovement mazePlayerMovement = mazePlayer.GetComponent<MazePlayerMovement>();
        if (mazePlayerMovement != null)
        {
            mazePlayerMovement.enabled = true;
        }
        pauseCanvas.SetActive(false);
    }

    public void Quit()
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }
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
