using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerMover : MonoBehaviour
{
    public GameObject whiteScreen;

    void Start()
    {
        whiteScreen = GameObject.Find("WhiteScreen");
    }

    public void TriggerWhiteScreen()
    {
        whiteScreen.SetActive(true);
    }
}
