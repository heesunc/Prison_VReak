using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class NowTime : MonoBehaviour
{
    public AudioClip soundClip;                        //오디오 클립 설정
    private AudioSource audioSource;                  //오디오 소스에 접근 하기
    private TextMeshProUGUI timerText;                //시간 나타내주는 텍스트에 접근 하기
    private float elapsedTime = 0f;                   //시간이 얼마나 지났는지 저장해주기 위한 변수

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        timerText = GetComponent<TextMeshProUGUI>();
        StartTimer();                                              //타이머 시작
    }

    private void StartTimer()                                     //타이머 시작 함수 생성
    {
        elapsedTime = 0f;                                         //경과 시간을 0으로 초기화
        UpdateTimerText();                                        //타이머 업데이트 함수 호출
        PlaySound();                                              //소리 재생 함수 호출
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            int hours = (int)(elapsedTime / 3600);                                                   //경과 시간에서 "시"를 계산
            int minutes = (int)((elapsedTime % 3600) / 60);                                          //경과 시간에서 "분"을 계산
            int seconds = (int)(elapsedTime % 60);                                                   //경과 시간에서 "초"를 계산
            string timeString = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);      //계산한 시, 분,초를 인덱스를 이용하여 불러옴. 형식은 00:00:00 모양을 띔. 
            timerText.text = timeString;                                                             //텍스트 업데이트
        }
    }

    private void PlaySound()                                             //오디오 재생
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
            elapsedTime += 1f;                                          //1초마다 경과 시간 업데이트
            UpdateTimerText();                                          //타이머 텍스트 업데이트
            yield return new WaitForSeconds(1f);                        //1초 기다림
        }
    }

    private void OnEnable()
    {
        StartCoroutine(UpdateTimerRoutine());                          //스크립트 활성화 할 때마다 시간 갱신
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