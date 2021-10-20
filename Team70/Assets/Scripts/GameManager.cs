using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // The general flow of our story is 
    // Player does action -> Trigger response -> Prompt next action
    // Probably don't have enough actions to warrant fully modularizing this

    public GrandmaController grandma;

    int currentEvent = 0;

    public void TriggerNextAction() 
    {
        switch (currentEvent) 
        {
            case 0: // Player walks through door -> wait for player to explore then trigger grandma
            case 1: // Player gets newspaper -> trigger grandma anim + dialogue
            case 2: // Player gets glasses -> trigger grandma anim + dialogue, wait, then anim + dialogue
            case 3: // Player gets meds -> trigger grandma anim + dialogue, grandma goes outside
            case 4: // Player goes outside -> trigger UI/o.w. to tell player to bark
            case 5: // Player barks -> trigger anim + dialogue
                grandma.TriggerNextState(); break;
            default: Debug.LogError("No Actions left!"); return;
        }

        currentEvent++;
    }
}
