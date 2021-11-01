using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMover : MonoBehaviour
{
    public GameObject whiteScreen;
    public AudioClip whiteScreenSound;
    public Material whiteSkyboxMaterial;
    bool isFade = false;
    AudioSource audioSource;
    Color now = Color.white;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (whiteScreen) whiteScreen.SetActive(false);
    }


    private void Update()
    {
        if (isFade)
        {
            now = RenderSettings.skybox.color;
            now = Color.Lerp(now, Color.white, 0.02f);
            RenderSettings.skybox.SetColor("_Tint", now);
        }
    }
    public void TriggerWhiteScreen()
    {
        if (whiteScreen)
        {
            //whiteScreen.SetActive(true);
            ChangeWhiteSkyBox();
        }
            
    }


    public void ChangeWhiteSkyBox()
    {
        RenderSettings.skybox.CopyPropertiesFromMaterial(whiteSkyboxMaterial);
        RenderSettings.skybox.SetColor("_Tint", new Color(150, 150, 150, 1f));
        isFade = true;
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
