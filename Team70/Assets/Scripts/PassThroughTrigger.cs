using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThroughTrigger : MonoBehaviour
{
    GameManager gameManager;
    BGMManager bgmManager;

    public bool startingScene;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        bgmManager = GameObject.Find("BGMManager").GetComponent<BGMManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!startingScene && other.tag == "Player")
        {
            gameManager.TriggerEndScene();
        }
        if (startingScene && other.tag == "Player")
        {
            gameManager.TriggerNextAction();
            Destroy(gameObject);
        }
    }
}
