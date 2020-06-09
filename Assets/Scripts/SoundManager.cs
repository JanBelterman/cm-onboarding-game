using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;

    public AudioSource audioSource;
    public AudioClip footstepSound;

    private void Awake() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(gameObject);
        }

    }
    public void PlayFootstep() {
        if(audioSource.isPlaying == false) {
            audioSource.volume = Random.Range(0.8f, 1);
            audioSource.pitch = Random.Range(0.8f, 1.1f);
            audioSource.PlayOneShot(footstepSound);
        }
        
    }
    
}
