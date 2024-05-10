using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UIElements;

public class RotateLevelOnClick : MonoBehaviour
{
    public static Vector2 activeRoom = new Vector2(0, 0);

    // FOR TESTING PURPOSES OF THE BASIC ROTATION

    public Vector3 pointOfRotation = new Vector3(5, 5, 0); // point of rotation
    public Vector3 axisOfRotation = Vector3.forward; // axis of rotation
    public float rotationAngle = 90f; // length of rotation
    public float rotationSpeed = 90f; // speed of rotation

    public static bool isRotating = false; // bool for if we are rotating
    private float currentRotation = 0f;

    public GameObject room;
    public GameObject switchBlocks;

    // FOR TESTING PURPOSES OF THE BASIC ROTATION
    private void Start()
    {
        isRotating = false;
    }

    void Update()
    {
        // Publish to rotate along X axis
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Publish events for the rotation and the UI
            EventBus.Publish<RotateEvent>(new RotateEvent(activeRoom, Vector3.zero, Vector3.zero));
            EventBus.Publish<LevelEvent>(new LevelEvent(0));

            if (!isRotating && !PlayerDamage.isDying)
            {
                /*
                isRotating = true;
                room.transform.Rotate(Vector3.forward, 90f);
                isRotating = false;
                */

                // Start the coroutine
                StartCoroutine(RotateOverTime(room.transform.rotation * Quaternion.Euler(0, 0, 90), 0.5f));
            }
        }

        // Publish to rotate along Y axis
        else if (Input.GetKeyDown(KeyCode.F))
        {
            // Publish events for the rotation and the UI
            EventBus.Publish<RotateEvent>(new RotateEvent(activeRoom, Vector3.zero, Vector3.zero));
            EventBus.Publish<LevelEvent>(new LevelEvent(0));

            if (!isRotating && !PlayerDamage.isDying)
            {
                // StartCoroutine(RotateAroundPoint(Vector3.up, 180f, 0.2f));
                
                foreach (Transform switchBlock in switchBlocks.transform)
                {
                    // Need to find the midpoint between the two switch blocks
                    Vector3 rotationPoint;

                    // Get the two blocks that need to be switched
                    Transform firstChild = switchBlock.GetChild(0);
                    Transform secondChild = switchBlock.GetChild(1);

                    rotationPoint = (firstChild.position + secondChild.position) / 2.0f;

                    // switchBlock.transform.RotateAround(rotationPoint, Vector3.up, 180f);

                    // Start the coroutine
                    StartCoroutine(RotateAroundPoint(rotationPoint, Vector3.up, 180f, 0.5f, switchBlock));
                }
            }
        }
    }

    private IEnumerator RotateOverTime(Quaternion toRotation, float duration)
    {
        isRotating = true;
        Quaternion startRotation = room.transform.rotation;
        float timePassed = 0f;

        while (timePassed < duration)
        {
            room.transform.rotation = Quaternion.Slerp(startRotation, toRotation, timePassed / duration);
            timePassed += Time.deltaTime;
            yield return null;
        }

        room.transform.rotation = toRotation; // Ensure the rotation is set precisely to the target rotation
        isRotating = false;
    }

    private IEnumerator RotateAroundPoint(Vector3 rotationPoint, Vector3 axis, float angle, float duration, Transform switchBlock)
    {
        isRotating = true;
        /*
        foreach (Transform switchBlock in switchBlocks.transform)
        {
            // Need to find the midpoint between the two switch blocks
            Vector3 rotationPoint;

            // Get the two blocks that need to be switched
            Transform firstChild = switchBlock.GetChild(0);
            Transform secondChild = switchBlock.GetChild(1);

            rotationPoint = (firstChild.position + secondChild.position) / 2.0f;
        */

        float rotateAmount = angle / duration; // Degrees per second

        Quaternion startRotation = switchBlock.transform.rotation;
        float totalRotation = 0;
        while (totalRotation < angle)
        {
            float deltaAngle = rotateAmount * Time.deltaTime;
            totalRotation += Mathf.Abs(deltaAngle);

            // Clamp the final rotation to not overshoot the desired angle
            if (totalRotation > angle)
            {
                deltaAngle -= totalRotation - angle;
            }

            switchBlock.transform.RotateAround(rotationPoint, axis, deltaAngle);

            yield return null;
        }

        // Snap to final rotation
        //switchBlock.transform.rotation = startRotation * Quaternion.AngleAxis(angle, axis);

        /*}*/
        isRotating = false;
    }

}
