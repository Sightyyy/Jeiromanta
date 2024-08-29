using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainHPInteract : MonoBehaviour
{
    private bool canInteract = true;
    public GameObject mazeCanvas;
    [SerializeField] private GameObject endPoint;
    [SerializeField] private GameObject mainDoor;
    [SerializeField] private GameObject hackPanel;
    private GameObject player;
    private bool mazeWin;

    private void Update()
    {
        mazeWin = endPoint.GetComponent<MazeWin>().isWin;
        if(mazeWin == true)
        {
            mazeCanvas.SetActive(false);
            MazeGenerator.DestroyMazePlayer();
            player = GameObject.Find("Player");
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.enabled = true;
            }
            mainDoor.SetActive(true);
            hackPanel.SetActive(false);
        }
    }
    
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
