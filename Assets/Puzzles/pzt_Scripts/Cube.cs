using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cube : MonoBehaviour
{
    public Canvas imageUI;                                          // Canvas 컴포넌트
    private bool isImageVisible = false;                         //처음에 false로 초기화 시킴. ((그 다음 동작에 따라 상태가 변경됨))

    private void Start()
    {
        if (imageUI != null)
        {
            imageUI.gameObject.SetActive(false);                           // 게임 처음 실행시켰을 때 이미지가 보이지 않게 설정
        }
    }

    private void OnMouseDown()
    {
        if (imageUI == null)
        {
            Debug.LogWarning("Canvas reference is not set!");                  //Canvas를 찾지 못하면 로그 출력
            return;
        }

        if (isImageVisible)
        {
            imageUI.gameObject.SetActive(false);                               //이미지가 보일 때는 안보이게 설정
        }
        else
        {
            imageUI.gameObject.SetActive(true);                               //이미지가 보이지 않을 때는 보이게 설정
        }
        isImageVisible = !isImageVisible;
    }
}