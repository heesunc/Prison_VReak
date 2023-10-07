using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMove : MonoBehaviour
{
    public float moveSpeed = 1.0f;

    void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
