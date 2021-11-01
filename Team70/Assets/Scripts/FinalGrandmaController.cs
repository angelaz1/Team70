using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalGrandmaController : MonoBehaviour
{
    public List<AudioClip> finalAudioClips;

    int currentClip = 0;
    AudioSource audioSource;
    EndingManager endingManager;
    BGMManager bgmManager;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        endingManager = GameObject.Find("EndingManager").GetComponent<EndingManager>();
        bgmManager = GameObject.Find("BGMManager").GetComponent<BGMManager>();
        StartCoroutine(PlayClips());
    }

    IEnumerator PlayClips()
    {
        while (currentClip < finalAudioClips.Count)
        {
            audioSource.clip = finalAudioClips[currentClip];
            if (currentClip == 5)
            {
                bgmManager.CutAllSounds();
                yield return new WaitForSeconds(1.5f);
            }
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length + 0.2f);
            currentClip++;
        }

        endingManager.TriggerCredits();
    }
}
