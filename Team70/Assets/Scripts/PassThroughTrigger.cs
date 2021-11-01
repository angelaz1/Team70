using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThroughTrigger : MonoBehaviour
{
    GameManager gameManager;

    public bool startingScene;
    public bool backyardTrigger;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (backyardTrigger && other.tag == "Player")
        {
            gameManager.SwapToBackyardBGM();
            Destroy(gameObject);
        }
        else if (startingScene && other.tag == "Player")
        {
            gameManager.TriggerNextAction();
            Destroy(gameObject);
        }
        else if (!startingScene && other.tag == "Player")
        {
            gameManager.TriggerEndScene();
            Destroy(gameObject);
        }
    }
}
