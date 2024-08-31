using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusic : MonoBehaviour
{
    AudioCollection audioCollection;


    public void Awake()
    {
        audioCollection = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioCollection>();
    }

    public void Start()
    {
        audioCollection.PlayBGM(audioCollection.level);
    }
}
