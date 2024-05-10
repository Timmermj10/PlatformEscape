using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    Subscription<LevelEvent> score_event_subscription;
    private GameObject information;

    void Start()
    {
        // Subscribe to the LevelEvents
        score_event_subscription = EventBus.Subscribe<LevelEvent>(_OnScoreUpdated);

        // Get the information game object
        information = GameObject.Find("Information");
    }

    void _OnScoreUpdated(LevelEvent e)
    {
        // This will be sent when the player clicks either X or Y
        if (e.newLevel == 0)
        {
            GetComponent<Text>().text = "";
        }

        else if (e.newLevel == 1)
        {
            information.GetComponent<Text>().text = "R = Rotate";
        }

        else if (e.newLevel == 3)
        {
            information.GetComponent<Text>().text = "F = Flip";
        }
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(score_event_subscription);
    }
}
