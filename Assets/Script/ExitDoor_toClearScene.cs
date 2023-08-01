using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor_toClearScene : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player와 충돌!");
        Debug.Log("Stage2_Scene으로 이동");
        SceneManager.LoadScene("Stage_2_Scene");
    }
}