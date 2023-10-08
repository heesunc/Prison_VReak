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
        if (transform.position != previousPosition)
        {
            timer += Time.deltaTime;
            if (timer >= soundInterval)
            {
                PlaySound();
                timer = 0f;
            }
            previousPosition = transform.position;
        }
    }

    void PlaySound()
    {
        audioSource.clip = soundClip;
        audioSource.Play();
    }
}
