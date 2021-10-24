using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    AudioSource audioSource;
    
    bool fadingOut = false;
    bool fadingIn = true;

    float fadeSpeed = 0.4f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (fadingOut)
        {
            fadingIn = false;
            audioSource.volume -= fadeSpeed * Time.deltaTime;
            if (audioSource.volume == 0) fadingOut = false;
        }

        else if (fadingIn)
        {
            audioSource.volume += fadeSpeed * Time.deltaTime;
            if (audioSource.volume == 1) fadingIn = false;
        }
    }

    public void FadeOutBGM()
    {
        fadingOut = true;
    }
}
