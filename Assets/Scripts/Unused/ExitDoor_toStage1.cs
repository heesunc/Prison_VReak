using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor_toStage1 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player와 충돌!");
        Debug.Log("Stage1로 이동");
        SceneManager.LoadScene("Stage_1_Scene");
    }
}