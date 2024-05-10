using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToNextLevel : MonoBehaviour
{
    static int nextLevel = 2;
    private string nextScene = "Level_";

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // Reset the current level incase of deaths or restarts
            string sceneName = SceneManager.GetActiveScene().name;
            nextLevel = int.Parse(sceneName[sceneName.Length - 1].ToString()) + 1;

            nextScene = nextScene + nextLevel;
            nextLevel++;
            SceneManager.LoadScene(nextScene);
        }
    }

    // Subscribe to restart event
    // If restart, reset nextLevel to 2
    /*
    Subscription<RestartEvent> restart_event_subscription;

    void Start()
    {
        restart_event_subscription = EventBus.Subscribe<RestartEvent>(_OnRestart);
    }

    void _OnRestart(RestartEvent e)
    {
        Debug.Log("HERE");
        nextLevel = 2;
    }
    */
}
