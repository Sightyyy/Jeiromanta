using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackPanelInteract : MonoBehaviour
{
    private bool canInteract = true;
    public GameObject mazeCanvas;
    public GameObject player;

    public void Interact()
    {
        if (canInteract)
        {
            Debug.Log("Interact!");
            OnInteract();
        }

        void OnInteract()
        {
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.enabled = false;
            }

            PlayerInteract playerInteract = player.GetComponent<PlayerInteract>();
            if (playerInteract != null)
            {
                playerInteract.enabled = false;
            }

            mazeCanvas.SetActive(true);
        }
    }
}
