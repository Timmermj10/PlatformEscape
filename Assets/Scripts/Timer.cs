using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // Timer base plate from UMGPT, modified it to get what I wanted out of it
    private Text stopwatchText;

    public static float elapsedTime;
    public static bool isRunning = false;

    private void Start()
    {
        stopwatchText = GameObject.Find("Stopwatch").GetComponent<Text>();

        if (isRunning)
        {
            stopwatchText.enabled = true;
            GameObject timerButton = GameObject.Find("Timer");
            if (timerButton != null)
            {
                timerButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.green;
            }
        }
    }

    private void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateStopwatchUI();
        }

        if (SceneManager.GetActiveScene().name == "Level_10")
        {
            isRunning = false;
        }
    }

    public void StartStopTimer()
    {
        if (!isRunning)
        {
            isRunning = true;
            stopwatchText.enabled = true;

            // Change the text to Green
            GameObject.Find("Timer").GetComponentInChildren<TextMeshProUGUI>().color = Color.green;
        }
        else 
        {
            isRunning = false;
            stopwatchText.enabled = false;
            ResetTimer();

            // Change the text to Red
            GameObject.Find("Timer").GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
        }
    }

    public void ResetTimer()
    {
        elapsedTime = 0;
        isRunning = false;
        UpdateStopwatchUI();
    }

    private void UpdateStopwatchUI()
    {
        // Format the time as you want (here, it's in minutes:seconds:milliseconds)
        int minutes = (int)(elapsedTime / 60);
        int seconds = (int)(elapsedTime % 60);

        stopwatchText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
