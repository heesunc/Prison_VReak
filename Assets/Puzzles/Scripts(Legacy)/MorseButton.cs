using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorseButton : MonoBehaviour
{
    public GameObject square;
    public GameObject dot;
    public GameObject square1;
    public GameObject square2;
    public GameObject square3;
    public GameObject square4;
    public GameObject square5;
    public GameObject square6;

    public GameObject redUI;
    public GameObject black1UI;

    public GameObject SquareButton;

    public void OnClickSquare1()
    {
        ToggleUI(square1);
        ToggleUI(dot);
        CheckPuzzleSolved();
    }

    public void OnClickSquare2()
    {
        ToggleUI(square2);
        ToggleUI(dot);
        CheckPuzzleSolved();
    }

    public void OnClickSquare3()
    {
        ToggleUI(square3);
        ToggleUI(dot);
        CheckPuzzleSolved();
    }

    public void OnClickSquare4()
    {
        ToggleUI(square4);
        ToggleUI(dot);
        CheckPuzzleSolved();
    }

    public void OnClickSquare5()
    {
        ToggleUI(square5);
        ToggleUI(dot);
        CheckPuzzleSolved();
    }

    public void OnClickSquare6()
    {
        ToggleUI(square6);
        ToggleUI(dot);
        CheckPuzzleSolved();
    }

    private void ToggleUI(GameObject uiObject)
    {
        bool isUIVisible = !uiObject.activeSelf;
        uiObject.SetActive(isUIVisible);
    }

    private bool isPuzzleSolved = false; // ������ �ذ�Ǿ����� ���θ� �����ϴ� ����

    private void CheckPuzzleSolved()
    {
        if (!square1.activeSelf && !square2.activeSelf && !square3.activeSelf && !square5.activeSelf && square4.activeSelf && square6.activeSelf)
        {
            isPuzzleSolved = true; // ������ �ذ�Ǿ����� ǥ��
        }
        else
        {
            isPuzzleSolved = false; // ������ �ذ���� �ʾ����� ǥ��
        }
    }

    public void OnClickSquareButton(GameObject obj)
    {
        CheckPuzzleSolved(); // ���� ���� ������Ʈ

        if (isPuzzleSolved)
        {
            redUI.SetActive(false);
            black1UI.SetActive(false);
            Debug.Log("������ �ذ�Ǿ����ϴ�");
            StartCoroutine(Waiting(obj));
        }
    }

    IEnumerator Waiting(GameObject obj){
        yield return new WaitForSeconds(1f);
        obj.SetActive(false);
    }
}