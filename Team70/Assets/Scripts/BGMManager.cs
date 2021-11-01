using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioClip musicBoxBGM;
    public AudioClip backyardBGM;

    public AudioSource mainAudioSource;
    public AudioSource effectsAudioSource;

    bool fadingOut = false;
    bool fadingIn = true;

    public float fadeSpeed = 0.3f;
    float maxVolume;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        maxVolume = 0.6f;
    }

    void Update()
    {
        if (fadingOut)
        {
            fadingIn = false;
            mainAudioSource.volume = Mathf.Clamp(mainAudioSource.volume - fadeSpeed * Time.deltaTime, 0, maxVolume);
            if (mainAudioSource.volume == 0) fadingOut = false;
        }

        else if (fadingIn)
        {
            mainAudioSource.volume = Mathf.Clamp(mainAudioSource.volume + fadeSpeed * Time.deltaTime, 0, maxVolume);
            if (mainAudioSource.volume >= maxVolume) fadingIn = false;
        }
    }

    public void FadeOutBGM()
    {
        fadingOut = true;
    }

    public void PlayMusicBoxBGM()
    {
        mainAudioSource.clip = musicBoxBGM;
        mainAudioSource.Play();
        effectsAudioSource.Play();
        maxVolume = 0.2f;
        fadingIn = true;
    }

    //public void PlayBackyardBGM()
    //{
    //    mainAudioSource.clip = backyardBGM;
    //    mainAudioSource.Play();
    //    fadingIn = true;
    //}
}
