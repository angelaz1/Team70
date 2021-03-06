using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DogSFXManager : MonoBehaviour
{
    public AudioClip[] happyClips;
    public AudioClip[] sadClips;
    public AudioClip[] indoorMovingClips;
    public AudioClip[] grassMovingClips;

    public AudioClip happyJumpClip;

    public float delayPlayTime = 0.5f;

    AudioSource audioSource;
    AudioClip[] movingClips;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        movingClips = indoorMovingClips;
    }

    public void PlayHappyClip() 
    {
        AudioClip clip = happyClips[Random.Range(0, happyClips.Length - 1)];
        audioSource.volume = 0.5f;
        audioSource.clip = clip;
        Invoke(nameof(PlayClip), delayPlayTime);
    }

    public void PlaySadClip()
    {
        AudioClip clip = sadClips[Random.Range(0, sadClips.Length - 1)];
        audioSource.volume = 0.5f;
        audioSource.clip = clip;
        Invoke(nameof(PlayClip), delayPlayTime);
    }
    /// <summary>
    /// 0 left paws 1right paws
    /// </summary>
    /// <param name="i"></param>
    public void PlayMovingClips(int i)
    {
        AudioClip clip = movingClips[i];
        audioSource.volume = 0.2f;
        audioSource.clip = clip;
        Invoke(nameof(PlayClip), delayPlayTime);
    }

    public void SwapToGrassClips()
    {
        movingClips = grassMovingClips;
    }

    public void PlayHappyJumpClip()
    {
        AudioClip clip = happyJumpClip;
        audioSource.volume = 0.5f;
        audioSource.clip = clip;
        Invoke(nameof(PlayClip), delayPlayTime);
    }

    void PlayClip()
    {
        audioSource.Play();
    }
}
