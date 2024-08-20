using UnityEngine;
using TMPro; // Tambahkan ini untuk menggunakan TextMeshPro
using UnityEngine.UI;

public class ChatBubbleCommand : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // Referensi ke komponen TextMeshPro untuk teks dialog
    public RectTransform bubbleBackground; // Referensi ke background panel dari bubble
    public Transform npcTransform; // Transform NPC
    public Canvas chatBubbleCanvas; // Referensi ke Canvas dari Chat Bubble

    public Vector3 offset = new Vector3(0, 2.0f, 0); // Offset untuk bubble
    public float displayDuration = 3f; // Durasi berapa lama bubble ditampilkan
    private float currentTime; // Timer untuk menonaktifkan bubble
    private bool isDisplayed = false; // Status apakah bubble sedang aktif

    void Start()
    {
        chatBubbleCanvas.gameObject.SetActive(false); // Pastikan bubble tidak aktif di awal
        // Set anchor dan pivot dari TextMeshPro ke tengah
        dialogueText.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        dialogueText.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        dialogueText.rectTransform.pivot = new Vector2(0.5f, 0.5f);
    }

    void Update()
    {
        if (isDisplayed)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= displayDuration)
            {
                chatBubbleCanvas.gameObject.SetActive(false); // Nonaktifkan bubble setelah durasi habis
                isDisplayed = false;
            }
            UpdateBubblePosition();
        }
    }

    public void SetText(string newText)
    {
        dialogueText.text = newText;
        UpdateBubbleSize();
        chatBubbleCanvas.gameObject.SetActive(true); // Aktifkan bubble saat teks di-set
        isDisplayed = true;
        currentTime = 0f; // Reset timer
    }

    void UpdateBubbleSize()
    {
        // Mengatur ukuran bubble berdasarkan ukuran teks ditambah padding
        Vector2 textSize = new Vector2(dialogueText.preferredWidth, dialogueText.preferredHeight);
        bubbleBackground.sizeDelta = textSize + new Vector2(20f, 10f); // Padding horizontal dan vertikal
        dialogueText.rectTransform.anchoredPosition = Vector2.zero; // Pastikan teks berada di tengah panel
    }

    void UpdateBubblePosition()
    {
        // Set posisi bubble di atas NPC
        Vector3 bubblePosition = npcTransform.position + offset;
        transform.position = bubblePosition;
    }
}
