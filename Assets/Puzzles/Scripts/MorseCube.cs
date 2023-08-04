using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MorseCube : MonoBehaviour
{
    public Canvas imageUI; // �ν����Ϳ��� Canvas ������Ʈ�� �Ҵ�

    private bool isImageVisible = false;

    private void Start()
    {
        // �̹��� UI ��Ȱ��ȭ
        if (imageUI != null)
        {
            imageUI.gameObject.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        if (imageUI == null)
        {
            Debug.LogWarning("Canvas reference is not set!");
            return;
        }

        if (isImageVisible)
        {
            imageUI.gameObject.SetActive(false);
        }
        else
        {
            imageUI.gameObject.SetActive(true);
        }
        isImageVisible = !isImageVisible;
    }
}