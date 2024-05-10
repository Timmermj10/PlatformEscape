using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{
    private ParticleSystem lavaSpurt;

    public static bool isDying = false;

    void Awake()
    {
        lavaSpurt = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Restart the level if the player touches Lava
        if (other.gameObject.tag is "Lava")
        {
            isDying = true;
            StartCoroutine(PlayerDeathSequence());
        }
    }

    IEnumerator PlayerDeathSequence()
    {
        PlayerMovement.playerControls = false;

        // Play the lavaSpurt
        lavaSpurt.Play();

        // Run the animation
        yield return StartCoroutine(PlayerDeathAnimation());

        // Update the is dying
        isDying = false;

        // Reload the level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator PlayerDeathAnimation()
    {
        yield return new WaitForSeconds(1f);
    }
}
