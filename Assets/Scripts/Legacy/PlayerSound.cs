using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioClip soundClip; // 소리 클립
    private AudioSource audioSource;
    private Vector3 previousPosition;
    private float soundInterval = 0.5f; // 발자국 사운드 재생 간격 설정
    private float timer = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        previousPosition = transform.position;
    }

    void Update()
    {
        // 현재 위치가 이전 위치와 다를 때 (움직일 때)
        if (transform.position != previousPosition)
        {
            // 타이머 변수에 경과한 시간 더하기
            timer += Time.deltaTime;
            // 타이머가 발자국 사운드 재생 간격 이상일 때 코드 실행
            if (timer >= soundInterval)
            {
                PlaySound(0.5f);
                timer = 0f;
            }
            // 현재 위치를 이전 위치로 업데이트
            previousPosition = transform.position;
        }
    }

    public void PlaySound(float pitch)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = soundClip;
            audioSource.pitch = pitch; // Set the pitch value based on the argument
            audioSource.Play();
        }        
    }
}
