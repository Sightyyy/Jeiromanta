using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fullscreen : MonoBehaviour
{
    public GameObject toggle; // Referensi ke objek toggle
    public int windowedWidth = 1280; // Lebar untuk mode windowed dengan rasio 16:9
    public int windowedHeight = 720; // Tinggi untuk mode windowed dengan rasio 16:9

    void Update()
    {
        // Cek status aktif dari objek toggle
        if (toggle.activeSelf)
        {
            SetFullscreen(false); // Pindah ke mode windowed dengan resolusi 16:9
        }
        else
        {
            SetFullscreen(true); // Pindah ke mode fullscreen
        }
    }

    public void SetFullscreen(bool isFullscreen)
    {
        if (isFullscreen)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            Screen.fullScreen = true;
        }
        else
        {
            Screen.SetResolution(windowedWidth, windowedHeight, false); // Mengatur resolusi ke 16:9
            Screen.fullScreen = false;
        }
    }
}

