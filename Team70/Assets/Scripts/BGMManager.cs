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
        effectsAudioSource.volume = 0;
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
            if (effectsAudioSource.volume == 0)
            {
                effectsAudioSource.clip = backyardBGM;
                effectsAudioSource.volume = 0.1f;
                effectsAudioSource.Play();
                effectsFadingIn = true;
                effectsFadingOut = false;
                effectsMaxVolume = 0.5f;
            } 
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
        effectsAudioSource.volume = 0.5f;
        effectsAudioSource.Play();
        Invoke(nameof(ActuallyPlayMusicBoxBGM), effectsAudioSource.clip.length + 0.1f);
    }

    void ActuallyPlayMusicBoxBGM()
    {
        mainAudioSource.clip = musicBoxBGM;
        mainAudioSource.Play();

        effectsAudioSource.clip = staticBGM;
        effectsAudioSource.volume = 0;
        effectsAudioSource.loop = true;
        effectsAudioSource.Play();

        maxVolume = 0.25f;
        effectsMaxVolume = 0.05f;

        fadingIn = true;
        effectsFadingIn = true;
    }

    public void SwapToBackyard()
    {
        effectsFadingOut = true;
    }
}
