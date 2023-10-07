using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending_Scene : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        string nextSceneName = "Ending_Scene";
        SceneManager.LoadScene(nextSceneName);
        Debug.Log("탈출!!");
    }
}
