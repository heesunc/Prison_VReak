using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float playerSpeed = 5.0f;
    public Transform cameraTransform;
    // public float horizontalInput;
    // public float verticalInput;
    public CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        moveDirection = cameraTransform.TransformDirection(moveDirection);
        moveDirection *= playerSpeed;
        moveDirection.y = 0;

        characterController.Move(moveDirection * Time.deltaTime);

        // transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed* verticalInput);
        // transform.Translate(Vector3.right * Time.deltaTime * playerSpeed * horizontalInput);

    }
}
