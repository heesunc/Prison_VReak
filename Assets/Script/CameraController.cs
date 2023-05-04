using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 2.0f; // 카메라 회전 속도
    private float zAngle = 0.0f; // z값의 회전 각도

    void Update()
    {
        float horizontalInput = Input.GetAxis("Mouse X"); // 마우스의 수평 이동량

        // z값의 회전 각도를 변경합니다.
        zAngle += horizontalInput * speed;

        // 회전 각도를 Quaternion으로 변환합니다.
        Quaternion rotation = Quaternion.Euler(0.0f, zAngle, 0.0f);

        // 카메라의 회전을 적용합니다.
        transform.rotation = rotation;
    }
}
