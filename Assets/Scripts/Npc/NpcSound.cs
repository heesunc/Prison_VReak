using UnityEngine;

public class NpcSound : MonoBehaviour
{
    public AudioClip soundClip; // �Ҹ� Ŭ��
    private AudioSource audioSource;
    private Vector3 previousPosition;
    private float soundInterval = 0.5f; // ���ڱ� ���� ��� ���� ����
    private float timer = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        previousPosition = transform.position;
    }

    void Update()
    {
        // ���� ��ġ�� ���� ��ġ�� �ٸ� �� (������ ��)
        if (transform.position != previousPosition)
        {
            // Ÿ�̸� ������ ����� �ð� ���ϱ�
            timer += Time.deltaTime;
            // Ÿ�̸Ӱ� ���ڱ� ���� ��� ���� �̻��� �� �ڵ� ����
            if (timer >= soundInterval)
            {
                PlaySound(0.5f);
                timer = 0f;
            }
            // ���� ��ġ�� ���� ��ġ�� ������Ʈ
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
