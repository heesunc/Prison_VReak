using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // public Transform player;
    // public Vector3 offset;

    public float sensitivity = 50f;
    public float rotationX;
    public float rotationY;

    void OnEnable(){
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnDisable(){
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // transform.position = player.position + offset;

        // Rotate camera with horizontal mouse movement
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        rotationX += mouseY * sensitivity * Time.deltaTime;
        rotationY += mouseX * sensitivity * Time.deltaTime;
        if(rotationX < -30f)
            rotationX = -30f;
        if(rotationX > 35f)
            rotationX = 35f;
        // transform.RotateAround(player.position, Vector3.up, mouseX * 5f);
        transform.eulerAngles = new Vector3(-rotationX, rotationY, 0);
    }
}