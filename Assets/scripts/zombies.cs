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

    Vector3 center;
    Vector3 target;
    float waitTimer;
    Animator anim;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        center = transform.position;
        PickNewTarget();
    }

    void Update()
    {

        if (waitTimer > 0f)
        {
            waitTimer -= Time.deltaTime;
            if (anim) anim.SetBool("walking", false);
            return;
        }
        if (anim) anim.SetBool("walking", true);


        Vector3 dir = target - transform.position;
        dir.y = 0f;
        if (dir.sqrMagnitude > 0.01f)
        {
            Quaternion look = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, look, turnSpeed * Time.deltaTime);
        }

        // walk toward it
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // arrived: wait, then choose a new point
        if (Vector3.Distance(transform.position, target) < 0.2f)
        {
            waitTimer = waitTime;
            PickNewTarget();
        }
    }

    void PickNewTarget()
    {
        Vector2 p = Random.insideUnitCircle * radius;
        target = new Vector3(center.x + p.x, transform.position.y, center.z + p.y);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Vector3 away = other.transform.position - transform.position;
        away.y = 0f;
        other.transform.position += away.normalized * pushDistance;
    }
}
