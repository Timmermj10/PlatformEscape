using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GiveDirectionsOnLoad : MonoBehaviour
{
    static int currentLevel = 0;

    void OnTriggerEnter(Collider other)
    {
        // Disable the box collider
        GetComponent<BoxCollider>().enabled = false;

        if (other.tag == "Player")
        {
            // Reset the current level incase of deaths or restarts
            string sceneName = SceneManager.GetActiveScene().name;
            currentLevel = int.Parse(sceneName[sceneName.Length - 1].ToString()) - 1;

            currentLevel++;
            EventBus.Publish<LevelEvent>(new LevelEvent(currentLevel));
        }
    }

    // Subscribe to restart event
    // If restart, reset currentLevel to 0
    /*
    Subscription<RestartEvent> restart_event_subscription;

    void Start()
    {
        restart_event_subscription = EventBus.Subscribe<RestartEvent>(_OnRestart);
    }

    void _OnRestart(RestartEvent e)
    {
        Debug.Log("HERE2");
        currentLevel = 0;
    }
    */
}

public class LevelEvent
{
    public int newLevel = 0;
    public LevelEvent(int _newLevel)
    {
        newLevel = _newLevel;
    }

    public override string ToString()
    {
        return "newLevel : " + newLevel;
    }
}
