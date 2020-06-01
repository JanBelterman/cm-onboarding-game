using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody _rb;
    AudioSource audioSource;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_rb.velocity.magnitude > 2f && audioSource.isPlaying == false) {
            audioSource.volume = Random.Range(0.8f, 1);
            audioSource.pitch = Random.Range(0.8f, 1.1f);
            audioSource.Play();
        }
    }
}
