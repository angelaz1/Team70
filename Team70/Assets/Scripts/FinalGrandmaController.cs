using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalGrandmaController : MonoBehaviour
{
    public List<AudioClip> finalAudioClips;

    int currentClip = 0;
    AudioSource audioSource;
    DogSFXManager dogSFXManager;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        dogSFXManager = GameObject.Find("DogSFXManager").GetComponent<DogSFXManager>();
        StartCoroutine(PlayClips());
    }

    IEnumerator PlayClips()
    {
        while (currentClip < finalAudioClips.Count)
        {
            audioSource.clip = finalAudioClips[currentClip];
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
            if (currentClip == 0) dogSFXManager.PlaySadClip();
            if (currentClip == 1) dogSFXManager.PlayHappyClip();
            yield return new WaitForSeconds(1f);
            currentClip++;
        }
    }
}
