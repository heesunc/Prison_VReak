using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.position + offset;

        // Rotate camera with horizontal mouse movement
        float mouseX = Input.GetAxis("Mouse X");
        transform.RotateAround(player.position, Vector3.up, mouseX * 5f);
    }
}