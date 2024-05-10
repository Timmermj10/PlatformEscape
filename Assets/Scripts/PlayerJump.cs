using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    // How far and how long to jump for
    public float moveDistance = 1.0f;
    public float moveDuration = 0.5f;

    private bool jumping = false;


    // Scale
    private float maxSizeMultiplier = 1.5f;
    private float sizeDuration = 0.5f;

    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !jumping)
        {
            StartCoroutine(MoveUpAndDown(transform.position, moveDistance, moveDuration));
        }
    }

    IEnumerator MoveUpAndDown(Vector3 startPosition, float distance, float duration)
    {
        jumping = true;
        // Calculate the target position which is distance units above the start position
        Vector3 targetPosition = startPosition + Vector3.back * distance;

        // Change scale
        StartCoroutine(ChangeSizeOverTime(maxSizeMultiplier, sizeDuration));

        // Move up
        yield return StartCoroutine(MoveOverTime(startPosition, targetPosition, duration));

        // Move down
        yield return StartCoroutine(MoveOverTime(targetPosition, startPosition, duration));

        jumping = false;
    }

    IEnumerator MoveOverTime(Vector3 start, Vector3 end, float duration)
    {
        float elapsedTime = 0;

        // ADD IN AN ADJUSTER FOR THE SCALE OF THE GUY


        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(start, end, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = end; // Ensure the object gets exactly to the end position
    }

    private IEnumerator ChangeSizeOverTime(float targetMultiplier, float overTime)
    {
        Vector3 targetScale = originalScale * targetMultiplier;
        Vector3 startScale = transform.localScale;
        float timer = 0.0f;

        // Increase size over time
        while (timer <= overTime)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, timer / overTime);
            timer += Time.deltaTime;
            yield return null;
        }

        // Ensure the final size is set
        transform.localScale = targetScale;

        // Reset timer and startScale for decreasing size
        timer = 0.0f;
        startScale = transform.localScale;

        // Decrease size back over time
        while (timer <= overTime)
        {
            transform.localScale = Vector3.Lerp(startScale, originalScale, timer / overTime);
            timer += Time.deltaTime;
            yield return null;
        }

        // Ensure the final size is the original size
        transform.localScale = originalScale;
    }
}
