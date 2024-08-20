using System.Collections; // Tambahkan ini untuk menggunakan IEnumerator
using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    private ChatBubbleCommand chatBubbleCommand;
    private bool canInteract = true; // Status apakah bisa melakukan interaksi
    public float interactionCooldown = 5f; // Cooldown interaksi untuk menghindari spam

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

    // Coroutine untuk memberikan cooldown pada interaksi
    IEnumerator InteractionCooldown()
    {
        canInteract = false;
        yield return new WaitForSeconds(interactionCooldown); // Tunggu sesuai dengan waktu cooldown
        canInteract = true; // Reset status interaksi
    }
}
