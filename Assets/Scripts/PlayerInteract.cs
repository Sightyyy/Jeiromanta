using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    AudioCollection audioCollection;
    public void Awake()
    {
        audioCollection = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioCollection>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E is pressed");
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
                        audioCollection.StopPlaySFX();
                    }
                }
                else if(collider.CompareTag("Interactable") && collider.name.StartsWith("HackPanel"))
                {
                    HackPanelInteract hpInteract = collider.GetComponent<HackPanelInteract>();
                    if (hpInteract != null)
                    {
                        Debug.Log("Pass");
                        hpInteract.Interact();
                        audioCollection.StopPlaySFX();
                    }
                }
                else if(collider.CompareTag("Interactable") && collider.name.StartsWith("Exit"))
                {
                    MainHPInteract mainHPInteract = collider.GetComponent<MainHPInteract>();
                    if (mainHPInteract != null)
                    {
                        Debug.Log("Pass");
                        mainHPInteract.Interact();
                        audioCollection.StopPlaySFX();
                    }
                }
            }
        }
    }
}
