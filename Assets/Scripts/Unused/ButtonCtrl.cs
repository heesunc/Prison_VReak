using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCtrl : MonoBehaviour
{
    public void OnClickExit(){
        Application.Quit();
        Debug.Log("Button Click");
    }
}
