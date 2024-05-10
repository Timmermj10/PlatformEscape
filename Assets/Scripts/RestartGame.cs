using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    void Restart()
    {
        // Restart the game by going back to the first scene
        SceneManager.LoadScene("Level_1");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject.tag == "Player")
            {
                // Send out event to anything that might need it
                // EventBus.Publish<RestartEvent>(new RestartEvent());

                // Click is within bounds of the GameObject's collider
                Restart();
            }
        }
    }
}

/*
public class RestartEvent
{
    public RestartEvent()
    {
    }
}
*/
