using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    AudioSource audioSource;
    
    bool fadingOut = false;
    bool fadingIn = true;

    float fadeSpeed = 0.5f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadingOut)
        {
            audioSource.volume -= fadeSpeed * Time.deltaTime;
            if (audioSource.volume == 0) fadingOut = false;
        }

        if (fadingIn)
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
