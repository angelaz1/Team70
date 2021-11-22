using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public Material mr;

    public void StartTheGame()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void OnHover()
    {
        mr.color = Color.blue;
    }
    public void LeaveHover()
    {
        this.GetComponent<MeshRenderer>().material.color = Color.white;
    }
}
