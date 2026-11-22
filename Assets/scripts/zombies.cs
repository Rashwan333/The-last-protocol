using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombies : MonoBehaviour
{
    public float radius = 6f;
    public float speed = 1.5f;
    public float turnSpeed = 3f;
    public float waitTime = 2f;
    public float pushDistance = 3f;

    [Header("Zombie Sound")]
    public AudioClip zombieSound;
    public float minVolume = 0.1f;
    public float maxVolume = 1f;
    public float dangerDistance = 3f;

    [Header("Player")]
    public Transform player;

    Vector3 center;
    Vector3 target;
    float waitTimer;
    Animator anim;

    AudioSource audioSource;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();

        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = zombieSound;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
        audioSource.volume = 0f;

        center = transform.position;
        PickNewTarget();
    }

    void Update()
    {
        if (waitTimer > 0f)
        {
            waitTimer -= Time.deltaTime;
        }

        Vector3 dir = target - transform.position;
        dir.y = 0f;

        if (dir.sqrMagnitude > 0.01f)
        {
            Quaternion look = Quaternion.LookRotation(dir);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                look,
                turnSpeed * Time.deltaTime
            );
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target) < 0.2f)
        {
            waitTimer = waitTime;
            PickNewTarget();
        }

        if (anim)
        {
            anim.SetBool("walking", true);
        }

        UpdateZombieSound();
    }

    void UpdateZombieSound()
    {
        if (player == null || zombieSound == null)
            return;

        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        if (distance > dangerDistance)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            return;
        }

        float danger = 1f - Mathf.Clamp01(
            distance / dangerDistance
        );

        float targetVolume = Mathf.Lerp(
            minVolume,
            maxVolume,
            danger
        );

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }

        audioSource.volume = Mathf.Lerp(
            audioSource.volume,
            targetVolume,
            Time.deltaTime * 3f
        );
    }

    void PickNewTarget()
    {
        Vector2 p = Random.insideUnitCircle * radius;

        target = new Vector3(
            center.x + p.x,
            transform.position.y,
            center.z + p.y
        );
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Vector3 away = other.transform.position - transform.position;
        away.y = 0f;

        other.transform.position += away.normalized * pushDistance;
    }
}