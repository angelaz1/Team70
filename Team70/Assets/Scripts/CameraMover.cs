using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public GameObject whiteScreen;

    void Start()
    {
        whiteScreen = GameObject.Find("WhiteScreen");
        whiteScreen.SetActive(false);
    }

    public void TriggerWhiteScreen()
    {
        whiteScreen.SetActive(true);
    }
}
