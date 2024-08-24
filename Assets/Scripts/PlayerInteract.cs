using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            float interactRange = 3f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {
                if(collider.CompareTag("Interactable") && collider.name.StartsWith("NPC"))
                {
                    NPCInteractable npcInteractable = collider.GetComponent<NPCInteractable>();
                    if (npcInteractable != null)
                    {
                        npcInteractable.Interact();
                    }
                }
                else if(collider.CompareTag("Interactable") && collider.name.StartsWith("Hack Panel"))
                {
                    HackPanelInteract hpInteract = collider.GetComponent<HackPanelInteract>();
                    if (hpInteract != null)
                    {
                        hpInteract.Interact();
                    }
                }
            }
        }
    }
}
