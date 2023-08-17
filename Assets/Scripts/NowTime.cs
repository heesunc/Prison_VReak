using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class NowTime : MonoBehaviour
{
    public AudioClip soundClip;                        //����� Ŭ�� ����
    private AudioSource audioSource;                  //����� �ҽ��� ���� �ϱ�
    private TextMeshProUGUI timerText;                //�ð� ��Ÿ���ִ� �ؽ�Ʈ�� ���� �ϱ�
    private float elapsedTime = 0f;                   //�ð��� �󸶳� �������� �������ֱ� ���� ����

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        timerText = GetComponent<TextMeshProUGUI>();
        StartTimer();                                              //Ÿ�̸� ����
    }

    private void StartTimer()                                     //Ÿ�̸� ���� �Լ� ����
    {
        elapsedTime = 0f;                                         //��� �ð��� 0���� �ʱ�ȭ
        UpdateTimerText();                                        //Ÿ�̸� ������Ʈ �Լ� ȣ��
        PlaySound();                                              //�Ҹ� ��� �Լ� ȣ��
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            int hours = (int)(elapsedTime / 3600);                                                   //��� �ð����� "��"�� ���
            int minutes = (int)((elapsedTime % 3600) / 60);                                          //��� �ð����� "��"�� ���
            int seconds = (int)(elapsedTime % 60);                                                   //��� �ð����� "��"�� ���
            string timeString = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);      //����� ��, ��,�ʸ� �ε����� �̿��Ͽ� �ҷ���. ������ 00:00:00 ����� ��. 
            timerText.text = timeString;                                                             //�ؽ�Ʈ ������Ʈ
        }
    }

    private void PlaySound()                                             //����� ���
    {
        if (soundClip != null && audioSource != null)
        {
            audioSource.clip = soundClip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    private IEnumerator UpdateTimerRoutine()
    {
        while (true)
        {
            elapsedTime += 1f;                                          //1�ʸ��� ��� �ð� ������Ʈ
            UpdateTimerText();                                          //Ÿ�̸� �ؽ�Ʈ ������Ʈ
            yield return new WaitForSeconds(1f);                        //1�� ��ٸ�
        }
    }

    private void OnEnable()
    {
        StartCoroutine(UpdateTimerRoutine());                          //��ũ��Ʈ Ȱ��ȭ �� ������ �ð� ����
    }

    private void OnDisable()
    {
        StopCoroutine(UpdateTimerRoutine());
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}