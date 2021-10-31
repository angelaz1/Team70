using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalGrandmaController : MonoBehaviour
{
    public List<AudioClip> finalAudioClips;

    int currentClip = 0;
    AudioSource audioSource;
    EndingManager endingManager;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        endingManager = GameObject.Find("EndingManager").GetComponent<EndingManager>();
        StartCoroutine(PlayClips());
    }

    IEnumerator PlayClips()
    {
        while (currentClip < finalAudioClips.Count)
        {
            audioSource.clip = finalAudioClips[currentClip];
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length + 0.2f);
            //if (currentClip == 0) dogSFXManager.PlaySadClip();
            //if (currentClip == 1) dogSFXManager.PlayHappyClip();
            //yield return new WaitForSeconds(1.5f);
            currentClip++;
        }

        endingManager.TriggerCredits();
    }
}
