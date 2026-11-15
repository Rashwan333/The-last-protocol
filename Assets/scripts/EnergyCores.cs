using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class chips : MonoBehaviour


{
    public static int score = 0;
    public int targetChips = 6;
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

            if (score >= targetChips)
            {
                Debug.Log("You Win!");
            }

            Destroy(gameObject);
        }



    }
}