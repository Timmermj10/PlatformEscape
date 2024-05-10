using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerMovement : MonoBehaviour
{
    // Holds the movement speed of the player
    public float movementSpeed = 4f;

    // Holds how long the movement animation should last
    private float movementDuration = 0.2f;

    // Holds the rb of the player
    Rigidbody rb;

    // Holds all possible movement directions
    private enum MovementDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    // Holds the last direction moved
    private static MovementDirection lastMovementDirection = MovementDirection.Up;

    // To hold wether the player controls are on
    public static bool playerControls = true;


    // Bool for first movement of the game
    private bool firstMovement = true;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerControls = true;
    }

    // Every frame check if we can move, if we can, move in the direction that the player is providing
    private void Update()
    {
        // If we are allowed to be moving
        if (playerControls)
        {
            GetInput();
        }
    }
    void GetInput()
    {
        float horizontal_input = Input.GetAxisRaw("Horizontal");

        float vertical_input = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(horizontal_input) > 0.0f)
            vertical_input = 0.0f;

        if ((vertical_input != 0.0f || horizontal_input != 0.0f) && playerControls)
        {
            // For removing the WASD
            if (firstMovement)
            {
                firstMovement = false;
                EventBus.Publish<LevelEvent>(new LevelEvent(0));
            }

            float horizontal_position = transform.position.x;
            float vertical_position = transform.position.y;

            Debug.Log(horizontal_position);
            Debug.Log(vertical_position);

            Vector2 startPosition = new Vector3(horizontal_position, vertical_position);

            if (vertical_input < 0.0f)
            {
                Debug.Log("move down");
                Vector2 endPosition = new Vector2(horizontal_position, vertical_position - 1);
                if (CanMoveTo(endPosition))
                {
                    StartCoroutine(MoveOverTime(startPosition, endPosition));
                }
            }
            else if (vertical_input > 0.0f)
            {
                Vector2 endPosition = new Vector2(horizontal_position, vertical_position + 1);
                if (CanMoveTo(endPosition))
                {
                    StartCoroutine(MoveOverTime(startPosition, endPosition));
                }
            }
            else if (horizontal_input < 0.0f)
            {
                Vector2 endPosition = new Vector2(horizontal_position - 1, vertical_position);
                if (CanMoveTo(endPosition))
                {
                    StartCoroutine(MoveOverTime(startPosition, endPosition));
                }
            }
            else if (horizontal_input > 0.0f)
            {
                Vector2 endPosition = new Vector2(horizontal_position + 1, vertical_position);
                if (CanMoveTo(endPosition))
                {
                    StartCoroutine(MoveOverTime(startPosition, endPosition));
                }
            }
        }
    }

    // Code from UMGPT for detecting if the collider is a trigger or not
    private bool CanMoveTo(Vector2 target)
    {
        Collider[] hitColliders = Physics.OverlapBox(target, transform.localScale / 2, Quaternion.identity);
        foreach (var hitCollider in hitColliders)
        {
            if (!hitCollider.isTrigger) // It's a solid collider, can't move there
            {
                return false;
            }
        }
        return true; // No solid colliders, can move
    }

    IEnumerator MoveOverTime(Vector2 start, Vector2 end)
    {
        Debug.Log("Trying to move");
        // Disable player controls while animation is happening
        playerControls = false;

        // Time variable
        float timeElapsed = 0;

        // While we havent been moving for the full duration
        while (timeElapsed < movementDuration)
        {
            transform.position = Vector2.Lerp(start, end, timeElapsed / movementDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        Debug.Log("moved");

        // Make sure we are at the end position
        transform.position = end;

        // Enable player controls
        if (!PlayerDamage.isDying)
        {
            playerControls = true;
        }
    }
}
