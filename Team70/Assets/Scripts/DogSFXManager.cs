using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DogSFXManager : MonoBehaviour
{
    public AudioClip[] happyClips;
    public AudioClip[] sadClips;

    float delayPlayTime = 0.5f;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayHappyClip() 
    {
        AudioClip clip = happyClips[Random.Range(0, happyClips.Length - 1)];
        audioSource.clip = clip;
        Invoke(nameof(PlayClip), delayPlayTime);
    }

    public void PlaySadClip()
    {
        AudioClip clip = sadClips[Random.Range(0, sadClips.Length - 1)];
        audioSource.clip = clip;
        Invoke(nameof(PlayClip), delayPlayTime);
    }

    void PlayClip()
    {
        audioSource.Play();
    }
}
