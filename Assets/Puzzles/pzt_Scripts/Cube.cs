using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cube : MonoBehaviour
{
    public Canvas imageUI;                                          // Canvas ������Ʈ
    private bool isImageVisible = false;                         //ó���� false�� �ʱ�ȭ ��Ŵ. ((�� ���� ���ۿ� ���� ���°� �����))

    private void Start()
    {
        if (imageUI != null)
        {
            imageUI.gameObject.SetActive(false);                           // ���� ó�� ��������� �� �̹����� ������ �ʰ� ����
        }
    }

    private void OnMouseDown()
    {
        if (imageUI == null)
        {
            Debug.LogWarning("Canvas reference is not set!");                  //Canvas�� ã�� ���ϸ� �α� ���
            return;
        }

        if (isImageVisible)
        {
            imageUI.gameObject.SetActive(false);                               //�̹����� ���� ���� �Ⱥ��̰� ����
        }
        else
        {
            imageUI.gameObject.SetActive(true);                               //�̹����� ������ ���� ���� ���̰� ����
        }
        isImageVisible = !isImageVisible;
    }
}