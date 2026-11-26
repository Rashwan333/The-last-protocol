using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnergyCores : MonoBehaviour
{
    public static int score = 0;
    public int targetCores = 6;
    public TMP_Text coreCounterText;

    public AudioClip collectSound;
    public float soundVolume = 1f;

    private Vector3 CoreRotator;

    void Start()
    {
        score = 0;
        CoreRotator = new Vector3(0, 0, 1);
        UpdateCounterText();

    }

    void Update()
    {
        transform.Rotate(CoreRotator);
        UpdateCounterText();
    }

    void UpdateCounterText()
    {
        if (coreCounterText != null)
        {
            coreCounterText.text = "Chips: " + score + "/" + targetCores;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            score++;
            Debug.Log("Score: " + score);

            if (coreCounterText != null)
            {
                coreCounterText.text = "Chips: " + score + "/" + targetCores;
            }

            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(
                    collectSound,
                    transform.position,
                    soundVolume
                );
            }

            if (score >= targetCores)
            {
                Debug.Log("You Win!");
            }

            Destroy(gameObject);
        }
    }
}