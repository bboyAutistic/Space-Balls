using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

public class PlayerController : MonoBehaviour {

    public float movementSpeed = 10;
    public float turningSpeed = 60;

    public Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal") * turningSpeed * Time.deltaTime;
        transform.Rotate(0, horizontal, 0);

        float vertical = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        transform.Translate(0, 0, vertical);

        if (Input.GetKeyDown(KeyCode.X))
        {
            this.rb.velocity = Vector3.zero;
            this.rb.angularVelocity = Vector3.zero;
        }
    }
}

