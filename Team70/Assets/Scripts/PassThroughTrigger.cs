using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThroughTrigger : MonoBehaviour
{
    GameManager gameManager;

    public bool startingScene;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!startingScene && other.tag == "Frisbee")
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
