using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMover : MonoBehaviour
{
    public GameObject whiteScreen;
    public AudioClip whiteScreenSound;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (whiteScreen) whiteScreen.SetActive(false);
    }

    public void TriggerWhiteScreen()
    {
        if (whiteScreen) whiteScreen.SetActive(true);
    }

    public void LoadEpilogueScene()
    {
        SceneManager.LoadScene("EndingScene");
    }

    public void TriggerWhiteScreenSFX()
    {
        if (audioSource)
        {
            audioSource.clip = whiteScreenSound;
            audioSource.Play();
        }
    }
}
