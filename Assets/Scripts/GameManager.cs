using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private DateTime startTime;

    private static GameManager instance;

    public GameObject renderStreamingObj;
    public GameObject npcObj;
    public GameObject endingObj;
    public bool isEmergency = false;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject gameManagerObject = new GameObject("GameManager");
                    instance = gameManagerObject.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    public void SetStartTime(DateTime time)
    {
        startTime = time;
    }

    public DateTime GetStartTime()
    {
        return startTime;
    }

    private void Awake()
    {
        if(GameObject.Find("EMERGENCYSITUATION") != null)
        {
            renderStreamingObj.SetActive(false);
            isEmergency = true;
        }
    }
}
