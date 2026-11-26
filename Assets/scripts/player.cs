using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    Animator anim;
    public float speed = 12f;
    public float runSpeed = 15f;
    public float mouseSensitivity = 2f;




    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, Input.GetAxis("Mouse X") * mouseSensitivity, 0f);
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        bool isMoving = new Vector2(x, z).magnitude > 0.1f;
        bool isRunning = isMoving && Input.GetKey(KeyCode.LeftShift);

        float currentSpeed = speed;
        if (isRunning)
        {
            currentSpeed = runSpeed;
        }
        else
        {
            currentSpeed = speed;
        }

        Vector3 movement = new Vector3(x, 0, z);
        transform.Translate(movement * currentSpeed * Time.deltaTime);


        anim.SetBool("walking", isMoving);
        anim.SetBool("running", isRunning);
    }
}
