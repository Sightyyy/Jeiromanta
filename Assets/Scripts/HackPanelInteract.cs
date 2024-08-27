using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackPanelInteract : MonoBehaviour
{
    private bool canInteract = true;
    public GameObject mazeCanvas;
    private GameObject player;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            Debug.Log("Hack Panel Interact");
        }
    }

    public void Interact()
    {
        if (canInteract)
        {
            Debug.Log("Interact!");
            OnInteract();
        }
    }

    public void OnInteract()
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        // PlayerInteract playerInteract = player.GetComponent<PlayerInteract>();
        // if (playerInteract != null)
        // {
        //     playerInteract.enabled = false;
        // }
        mazeCanvas.SetActive(true);
        mazeCanvas.GetComponentInChildren<MazeGenerator>().DestroyMazeNodes();
        MazeGenerator.DestroyMazePlayer();
        mazeCanvas.GetComponentInChildren<MazeGenerator>().NewMaze();
    }
}
