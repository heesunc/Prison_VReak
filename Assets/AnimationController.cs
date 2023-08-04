using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    public string animationParameter = "Speed";
    public float positionThreshold = 0.001f;

    private Vector3 previousPosition;

    void Start()
    {
        previousPosition = transform.position;
    }

    void Update()
    {
        if (animator != null)
        {
            // 현재 프레임과 이전 프레임의 오브젝트 위치를 비교하여 속도 계산
            Vector3 currentPosition = transform.position;
            float distanceMoved = Vector3.Distance(currentPosition, previousPosition);
            float moveSpeed = distanceMoved / Time.deltaTime;

            // Animator의 해당 파라미터 값 설정
            if (moveSpeed > positionThreshold)
            {
                animator.SetFloat(animationParameter, moveSpeed);
            }
            else
            {
                animator.SetFloat(animationParameter, 0f);
            }

            previousPosition = currentPosition;
        }
    }
}
