using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioClip musicBoxBGM;

    AudioSource audioSource;
    
    bool fadingOut = false;
    bool fadingIn = true;

    float fadeSpeed = 0.4f;
    public float maxVolume = 0.6f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (fadingOut)
        {
            fadingIn = false;
            audioSource.volume = Mathf.Clamp(audioSource.volume - fadeSpeed * Time.deltaTime, 0, maxVolume);
            if (audioSource.volume == 0) fadingOut = false;
        }

        else if (fadingIn)
        {
            audioSource.volume = Mathf.Clamp(audioSource.volume + fadeSpeed * Time.deltaTime, 0, maxVolume);
            if (audioSource.volume >= maxVolume) fadingIn = false;
        }
    }

    public void FadeOutBGM()
    {
        fadingOut = true;
    }

    public void PlayMusicBoxBGM()
    {
        audioSource.clip = musicBoxBGM;
        fadingIn = true;
    }
}
