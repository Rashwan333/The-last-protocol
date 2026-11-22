using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chips : MonoBehaviour
{
    public static int score = 0;
    public int targetChips = 6;

    public AudioClip collectSound;
    public float soundVolume = 1f;

    private Vector3 CoreRotator;

    void Start()
    {
        score = 0;
        CoreRotator = new Vector3(0, 0, 1);
    }

    void Update()
    {
        transform.Rotate(CoreRotator);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            score++;

            Debug.Log("Score: " + score);

            // Play coin collection sound
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(
                    collectSound,
                    transform.position,
                    soundVolume
                );
            }

            if (score >= targetChips)
            {
                Debug.Log("You Win!");
            }

            Destroy(gameObject);
        }
    }
}