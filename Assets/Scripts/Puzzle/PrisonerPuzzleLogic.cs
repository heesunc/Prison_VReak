using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrisonerPuzzleLogic : MonoBehaviour
{
    public GameObject photo;                                          //�ش� ������̵� ����
    public GameObject photo1;                                         //Steve ����
    public GameObject photo2;                                         //Logan ����
    public GameObject photo3;                                         //Kim ����
    public GameObject photo4;                                         //Cindy ����
    public GameObject photo5;                                         //Jack ����
    public GameObject photo6;                                         //Rachel ����

    public PuzzleMainFrame puzzleMainFrame;                                   //PUSH�� �����ִ� �׸� ����� ��ư UI

    public void OnClickPhoto1()                                      //UI�� ����ϰ� ���� ���¸� ������Ʈ �ϴ� �Լ�
    {
        ToggleUI(photo1);
        CheckPuzzleSolved();
    }

    public void OnClickPhoto2()                                      //UI�� ����ϰ� ���� ���¸� ������Ʈ �ϴ� �Լ�
    {
        ToggleUI(photo2);
        CheckPuzzleSolved();
    }

    public void OnClickPhoto3()                                      //UI�� ����ϰ� ���� ���¸� ������Ʈ �ϴ� �Լ�
    {
        ToggleUI(photo3);
        CheckPuzzleSolved();
    }

    public void OnClickPhoto4()                                      //UI�� ����ϰ� ���� ���¸� ������Ʈ �ϴ� �Լ�
    {
        ToggleUI(photo4);
        CheckPuzzleSolved();
    }

    public void OnClickPhoto5()                                      //UI�� ����ϰ� ���� ���¸� ������Ʈ �ϴ� �Լ�
    {
        ToggleUI(photo5);
        CheckPuzzleSolved();
    }

    public void OnClickPhoto6()                                      //UI�� ����ϰ� ���� ���¸� ������Ʈ �ϴ� �Լ�
    {
        ToggleUI(photo6);
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
        if (!photo5.activeSelf && !photo6.activeSelf && photo1.activeSelf && photo2.activeSelf && photo3.activeSelf && photo4.activeSelf)
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
        puzzleMainFrame.SetLightCoverUI(isPuzzleSolved);
    }
}