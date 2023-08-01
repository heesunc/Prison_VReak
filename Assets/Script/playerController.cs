using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    Rigidbody rb;
    float jumpForce = 250.0f;
    float walkForce = 2.0f;
    float maxSpeed = 5.0f;
    Transform cameraParent; // 카메라 부모 오브젝트
    CameraFollow cameraFollow;

    // Start is called before the first frame update
    void Start()
    {
        //this.transform.position = new Vector3(-4, 1, 2);
        this.rb = GetComponent<Rigidbody>();
        this.cameraParent = new GameObject("CameraParent").transform; // 카메라 부모 오브젝트 생성
        this.cameraFollow = Camera.main.GetComponent<CameraFollow>();
        Camera.main.transform.SetParent(this.cameraParent); // 카메라를 부모 오브젝트에 부착
    }

    // Update is called once per frame
    void FixedUpdate()
{
    // 이동
    float moveHorizontal = Input.GetAxis("Horizontal");
    float moveVertical = Input.GetAxis("Vertical");

    Vector3 moveDirection = new Vector3(moveHorizontal, 0, moveVertical);
    moveDirection = Camera.main.transform.TransformDirection(moveDirection);
    moveDirection.y = 0f;
    moveDirection.Normalize();

    Vector3 desiredVelocity = moveDirection * maxSpeed;
    Vector3 velocityChange = desiredVelocity - rb.velocity;
    velocityChange.y = 0f;

    rb.AddForce(velocityChange, ForceMode.VelocityChange);

    // 회전
    float rotateAmount = Input.GetAxis("Mouse X") * 3.0f;
    this.transform.Rotate(0, rotateAmount, 0);
}

}
