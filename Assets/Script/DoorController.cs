using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject player;
    public GameObject DoorButton;
    public GameObject targetObject;
    void Start()
    {
        targetObject.SetActive(false);
    }

    void OnMouseDown()
    {
        Vector3 DoorB = DoorButton.transform.position;
        Vector3 User = player.transform.position;
        float distance = Vector3.Distance(DoorB, User);
        if(distance <= 10f)
        {
            targetObject.SetActive(true);
        }         
    }
}
