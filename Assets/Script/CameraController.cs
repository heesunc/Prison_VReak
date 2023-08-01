using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float horizontalSpeed = 2.0f; // 수평 회전 속도
    public float verticalSpeed = 2.0f; // 수직 회전 속도
    private float xAngle = 0.0f; // x값의 회전 각도
    private float yAngle = 0.0f; // y값의 회전 각도

    void Update()
    {
        float horizontalInput = Input.GetAxis("Mouse X"); // 마우스의 수평 이동량
        float verticalInput = Input.GetAxis("Mouse Y"); // 마우스의 수직 이동량

        // 회전 각도를 변경합니다.
        xAngle -= verticalInput * verticalSpeed;
        yAngle += horizontalInput * horizontalSpeed;

        // x값의 회전 각도를 제한합니다.
        xAngle = Mathf.Clamp(xAngle, -90f, 90f);

        // y값의 회전 각도를 제한합니다.
        if (yAngle < 0f) yAngle += 360f;
        else if (yAngle > 360f) yAngle -= 360f;

        // 회전 각도를 Quaternion으로 변환합니다.
        Quaternion rotation = Quaternion.Euler(xAngle, yAngle, 0);

        // 카메라의 회전을 적용합니다.
        transform.rotation = rotation;
    }
}
