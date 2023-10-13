using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MorseNowTime : MonoBehaviour
{
    public AudioClip soundClip;
    private AudioSource audioSource;
    private TextMeshProUGUI dateTimeText;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        dateTimeText = GetComponent<TextMeshProUGUI>();
        GetCurrentTime();
    }

    private void GetCurrentTime()
    {
        if (dateTimeText != null)
        {
            dateTimeText.text = DateTime.Now.ToString("HH:mm:ss");
            PlaySound();
        }
    }

    private void PlaySound()
    {
        if (soundClip != null && audioSource != null)
        {
            audioSource.clip = soundClip;
            audioSource.Play();
        }
    }

    private IEnumerator UpdateTimeRoutine()
    {
        while (true)
        {
            GetCurrentTime();
            yield return new WaitForSeconds(1f);
        }
    }
    private void OnEnable()
    {
        StartCoroutine(UpdateTimeRoutine());
    }

    private void OnDisable()
    {
        StopCoroutine(UpdateTimeRoutine());
    }
}