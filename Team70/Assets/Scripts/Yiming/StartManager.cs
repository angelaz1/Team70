using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{

    public void StartTheGame()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void OnHover()
    {
        this.GetComponent<MeshRenderer>().material.color = Color.red;
    }
    public void LeaveHover()
    {
        this.GetComponent<MeshRenderer>().material.color = Color.white;
    }
}
