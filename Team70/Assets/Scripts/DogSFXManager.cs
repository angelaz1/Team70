using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DogSFXManager : MonoBehaviour
{
    public AudioClip[] happyClips;
    public AudioClip[] sadClips;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayHappyClip() 
    {
        AudioClip clip = happyClips[Random.Range(0, happyClips.Length - 1)];
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlaySadClip()
    {
        AudioClip clip = sadClips[Random.Range(0, sadClips.Length - 1)];
        audioSource.clip = clip;
        audioSource.Play();
    }
}
