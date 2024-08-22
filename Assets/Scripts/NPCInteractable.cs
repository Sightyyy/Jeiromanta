using System.Collections;
using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    private ChatBubbleCommand chatBubbleCommand;
    private bool canInteract = true;
    public float interactionCooldown = 5f;

    void Start()
    {
        chatBubbleCommand = GetComponent<ChatBubbleCommand>();
    }

    public void Interact()
    {
        if (canInteract)
        {
            Debug.Log("Interact!");
            OnInteract();
        }
    }

    void OnInteract()
    {
        if (chatBubbleCommand != null)
        {
            chatBubbleCommand.SetText("Greetings, traveler!");
            StartCoroutine(InteractionCooldown());
        }
    }

    IEnumerator InteractionCooldown()
    {
        canInteract = false;
        yield return new WaitForSeconds(interactionCooldown);
        canInteract = true;
    }
}
