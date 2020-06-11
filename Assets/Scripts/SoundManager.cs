using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;

    public AudioSource audioSource;
    public AudioClip footstepSound;

    public AudioClip menuHoverSound;
    public AudioClip menuSelectionSound1;
    public AudioClip menuSelectionSound2;
    public AudioClip menuSelectionSound3;
    public AudioClip questCompleteSound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    public void PlayFootstep()
    {
        if (audioSource.isPlaying == false)
        {
            audioSource.volume = Random.Range(0.8f, 1);
            audioSource.pitch = Random.Range(0.8f, 1.1f);
            audioSource.PlayOneShot(footstepSound);
        }

    }
    public void PlayMenuHover()
    {
        audioSource.PlayOneShot(menuHoverSound);
    }
    public void PlayMenuSelection1()
    {
        audioSource.PlayOneShot(menuSelectionSound1);
    }
    public void PlayMenuSelection2()
    {
        audioSource.PlayOneShot(menuSelectionSound2);
    }
    public void PlayMenuSelection3()
    {
        audioSource.PlayOneShot(menuSelectionSound3);
    }
    public void PlayQuestCompleteSound()
    {
        audioSource.PlayOneShot(questCompleteSound);
    }
}
