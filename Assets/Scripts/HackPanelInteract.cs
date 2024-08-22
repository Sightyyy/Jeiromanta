using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackPanelInteract : MonoBehaviour
{
    private bool canInteract = true;
    public GameObject Minigame;
    public GameObject Player;

    public void Interact()
    {
        if (canInteract)
        {
            Debug.Log("Interact!");
            OnInteract();
        }

        void OnInteract()
        {
            PlayerMovement playerMovement = Player.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.enabled = false;
            }

            PlayerInteract playerInteract = Player.GetComponent<PlayerInteract>();
            if (playerInteract != null)
            {
                playerInteract.enabled = false;
            }

            Minigame.SetActive(true);
        }
    }
}
