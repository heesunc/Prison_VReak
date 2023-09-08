using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    private float speed = 5f;
    private float rotationSpeed = 90f; // 회전하는 속도 변수 (장애물 감지하여 회피 시 사용)
    private float raycastDistance = 2.5f;
    private Rigidbody rigid;
    private Vector3 vector;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        RaycastHit hitInfo;

        Vector3 raycastDirection = transform.forward;

        Debug.DrawRay(rigid.position, raycastDirection * raycastDistance, Color.red);
        // Debug.DrawRay(rigid.position, vector.normalized * interactionDistance, Color.green);

        int obstacleLayerMask = LayerMask.GetMask("Obstacle");

        // 레이캐스트를 통해 장애물 감지
        if (Physics.Raycast(transform.position, raycastDirection, out hitInfo, raycastDistance, obstacleLayerMask))
        {
            // 장애물을 감지한 경우 회전
            Vector3 directionToAvoid = Vector3.Reflect(raycastDirection, hitInfo.normal);
            transform.rotation = Quaternion.LookRotation(directionToAvoid, Vector3.up);
        }
    }
}
