using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioCollection : MonoBehaviour
{
    [Header("========== Output ==========")]
    [SerializeField] AudioSource BGM;
    [SerializeField] AudioSource SFX;

    [Header("========== Background Music ==========")]
    public AudioClip mainMenu;
    public AudioClip inGame;
    public AudioClip level;
    
    [Header("========== SFX ==========")]
    public AudioClip UIButtonClick;
    public AudioClip UIBackButtonClick;
    public AudioClip walk;
    public AudioClip levelEnter;
    public AudioClip glitched;

    public void ButtonPress()
    {
        SFX.PlayOneShot(UIButtonClick);
    }

    public void BackButtonPress()
    {
        SFX.PlayOneShot(UIBackButtonClick);
    }
    
    public void PlayBGM(AudioClip clip)
    {
        BGM.clip = clip;
        BGM.loop = true;
        BGM.Play();
    }

    public void StopPlayBGM()
    {
        BGM.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFX.PlayOneShot(clip);
    }

}
