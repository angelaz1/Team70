using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioClip musicBoxBGM;
    public AudioClip backyardBGM;
    public AudioClip staticBGM;

    public AudioSource mainAudioSource;
    public AudioSource effectsAudioSource;

    bool fadingOut = false;
    bool fadingIn = true;
    bool effectsFadingOut = false;
    bool effectsFadingIn = false;

    public float fadeSpeed = 0.3f;
    float maxVolume;
    float effectsMaxVolume;

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

        if (effectsFadingOut)
        {
            effectsFadingIn = false;
            effectsAudioSource.volume = Mathf.Clamp(effectsAudioSource.volume - fadeSpeed * Time.deltaTime, 0, effectsMaxVolume);
            if (effectsAudioSource.volume == 0) effectsFadingOut = false;
        }
        else if (effectsFadingIn)
        {
            effectsAudioSource.volume = Mathf.Clamp(effectsAudioSource.volume + fadeSpeed * Time.deltaTime, 0, effectsMaxVolume);
            if (effectsAudioSource.volume >= maxVolume) effectsFadingIn = false;
        }
    }

    public void FadeOutBGM()
    {
        fadingOut = true;
    }

    public void PlayMusicBoxBGM()
    {
        effectsAudioSource.volume = 0.6f;
        effectsAudioSource.Play();
        Invoke(nameof(ActuallyPlayMusicBoxBGM), effectsAudioSource.clip.length);
        
    }

    void ActuallyPlayMusicBoxBGM()
    {
        mainAudioSource.clip = musicBoxBGM;
        mainAudioSource.Play();

        effectsAudioSource.clip = staticBGM;
        effectsAudioSource.volume = 0;
        effectsAudioSource.loop = true;
        effectsAudioSource.Play();

        maxVolume = 0.2f;
        effectsMaxVolume = 0.1f;

        fadingIn = true;
        effectsFadingIn = true;
    }

    public void SwapToBackyard()
    {
        effectsFadingOut = true;
    }

    //public void PlayBackyardBGM()
    //{
    //    mainAudioSource.clip = backyardBGM;
    //    mainAudioSource.Play();
    //    fadingIn = true;
    //}
}
