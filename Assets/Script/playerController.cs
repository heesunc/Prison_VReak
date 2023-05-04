using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    Rigidbody rb;
    float jumpForce = 250.0f;
    float walkForce = 2.0f;
    CameraFollow cameraFollow;

    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        this.cameraFollow = Camera.main.GetComponent<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && this.rb.velocity.y == 0.0f)
        {
            this.rb.AddForce(transform.up * this.jumpForce);
        }

        // move
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection += this.cameraFollow.transform.right;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection -= this.cameraFollow.transform.right;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveDirection += this.cameraFollow.transform.forward;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            moveDirection -= this.cameraFollow.transform.forward;
        }

        if (moveDirection.magnitude > 0)
        {
            moveDirection.Normalize();
            this.rb.AddForce(moveDirection * this.walkForce);
        }
    }
}
