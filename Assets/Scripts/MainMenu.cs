using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    AudioCollection audioCollection;

    public void Awake()
    {
        audioCollection = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioCollection>();
    }

    private void Start()
    {
        audioCollection.PlayBGM(audioCollection.mainMenu);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
