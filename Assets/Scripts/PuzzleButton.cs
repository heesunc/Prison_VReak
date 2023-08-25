using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButton : MonoBehaviour                                     //���� ����
{
    public GameObject square;                                          //�ش� ��ư(���� ����� ��ư)�� �ٷ� �ؿ� �ڸ��ϰ� �ִ� ȸ�� �׸����� UI
    public GameObject dot;                                             //ȸ�� �׸����� UI ���� �ڸ��ϰ� �ִ� �� UI
    public GameObject square1;                                         //7�� �ٿ��� ������ ȸ�� �׸� UI�� �� ���ʿ��� ù��°�� ��
    public GameObject square2;                                         //7�� �ٿ��� ������ ȸ�� �׸� UI�� �� ���ʿ��� �ι�°�� ��
    public GameObject square3;                                         //7�� �ٿ��� ������ ȸ�� �׸� UI�� �� ���ʿ��� ����°�� ��
    public GameObject square4;                                         //7�� �ٿ��� ������ ȸ�� �׸� UI�� �� ���ʿ��� �׹�°�� ��
    public GameObject square5;                                         //7�� �ٿ��� ������ ȸ�� �׸� UI�� �� ���ʿ��� �ټ���°�� ��
    public GameObject square6;                                         //7�� �ٿ��� ������ ȸ�� �׸� UI�� �� ���ʿ��� ������°�� ��

    public GameObject redUI;                                            //���� �� UI
    public GameObject black1UI;                                         //�ʷ� ���� ������ �ִ� ������ UI

    public GameObject SquareButton;                                    //PUSH�� �����ִ� �׸� ����� ��ư UI

    public void OnClickSquare1()                                      //UI�� ����ϰ� ���� ���¸� ������Ʈ �ϴ� �Լ�
    {
        ToggleUI(square1);
        ToggleUI(dot);
        CheckPuzzleSolved();
    }

    public void OnClickSquare2()                                      //UI�� ����ϰ� ���� ���¸� ������Ʈ �ϴ� �Լ�
    {
        ToggleUI(square2);
        ToggleUI(dot);
        CheckPuzzleSolved();
    }

    public void OnClickSquare3()                                      //UI�� ����ϰ� ���� ���¸� ������Ʈ �ϴ� �Լ�
    {
        ToggleUI(square3);
        ToggleUI(dot);
        CheckPuzzleSolved();
    }

    public void OnClickSquare4()                                      //UI�� ����ϰ� ���� ���¸� ������Ʈ �ϴ� �Լ�
    {
        ToggleUI(square4);
        ToggleUI(dot);
        CheckPuzzleSolved();
    }

    public void OnClickSquare5()                                      //UI�� ����ϰ� ���� ���¸� ������Ʈ �ϴ� �Լ�
    {
        ToggleUI(square5);
        ToggleUI(dot);
        CheckPuzzleSolved();
    }

    public void OnClickSquare6()                                      //UI�� ����ϰ� ���� ���¸� ������Ʈ �ϴ� �Լ�
    {
        ToggleUI(square6);
        ToggleUI(dot);
        CheckPuzzleSolved();
    }

    private void ToggleUI(GameObject uiObject)                        //UI ����ϴ� �Լ� ����
    {
        bool isUIVisible = !uiObject.activeSelf;
        uiObject.SetActive(isUIVisible);
    }

    private bool isPuzzleSolved = false;                            // ������ �ذ�Ǿ����� ���θ� �����ϴ� ����. �ʱ� ���� false�� ����

    private void CheckPuzzleSolved()
    {
        if (!square1.activeSelf && !square2.activeSelf && !square3.activeSelf && !square5.activeSelf && square4.activeSelf && square6.activeSelf)
        {
            isPuzzleSolved = true;                                    // ������ �ذ�Ǿ����� ǥ��
        }
        else
        {
            isPuzzleSolved = false;                                   // ������ �ذ���� �ʾ����� ǥ��
        }
    }

    public void OnClickSquareButton()
    {
        CheckPuzzleSolved();                                               // ���� ���� üũ

        if (isPuzzleSolved)                                               //���� �ذ� ������ �޼��ϸ�
        {
            redUI.SetActive(false);                                      //���� �� UI ��Ȱ��ȭ
            black1UI.SetActive(false);
            Debug.Log("������ �ذ�Ǿ����ϴ�");
        }
    }
}