using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform target;
    public float distance = 6f;
    public float height = 4f;
    public float lookHeight = 3f;
    // Start is called before the first frame update
    public GameObject Mech;
    void Start()
    {

    }

    // Update is called once per frame

    void Update()
    {
        transform.position = target.position - target.forward * distance + Vector3.up * height;
        transform.LookAt(target.position + Vector3.up * lookHeight);
    }
}
