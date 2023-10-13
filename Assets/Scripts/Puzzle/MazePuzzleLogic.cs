using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazePuzzleLogic : MonoBehaviour
{
    public int upCount = 0;                                         //ȭ��ǥ Ŭ�� Ƚ�� ���� ī��Ʈ
    public int downCount = 0;
    public int rightCount = 0;
    public int leftCount = 0;

    public GameObject First;                                       //���� �ٲ�� ���� ����
    public GameObject Second;
    public GameObject Third;
    public GameObject Fourth;
    public GameObject Fifth;

    public GameObject Wrong;                                      //���ǰ� ���� �ʴ� ��ư Ŭ�� �� ���� ó��

    public GameObject Mist_U;                                     //Reset ��ư ���� �� �ʱ�ȭ ���ֱ� ����
    public GameObject Mist_D;
    public GameObject Mist_R;
    public GameObject Mist_L;

    public PuzzleMainFrame puzzleMainFrame;

    public GameObject Red_dot1; public GameObject Blue_dot1;
    public GameObject Red_dot2; public GameObject Blue_dot2;
    public GameObject Red_dot3; public GameObject Blue_dot3;
    public GameObject Red_dot4; public GameObject Blue_dot4;

    private void Start()
    {
        int randomValue = Random.Range(0, 4);

        if(randomValue == 0)
        {
            Red_dot1.SetActive(true); Blue_dot1.SetActive(true);
            Red_dot2.SetActive(false); Blue_dot2.SetActive(false);
            Red_dot3.SetActive(false); Blue_dot3.SetActive(false);
            Red_dot4.SetActive(false); Blue_dot4.SetActive(false);
        }

        else if(randomValue == 1)
        {
            Red_dot1.SetActive(false); Blue_dot1.SetActive(false);
            Red_dot2.SetActive(true); Blue_dot2.SetActive(true);
            Red_dot3.SetActive(false); Blue_dot3.SetActive(false);
            Red_dot4.SetActive(false); Blue_dot4.SetActive(false);
        }

        else if(randomValue == 2)
        {
            Red_dot1.SetActive(false); Blue_dot1.SetActive(false);
            Red_dot2.SetActive(false); Blue_dot2.SetActive(false);
            Red_dot3.SetActive(true); Blue_dot3.SetActive(true);
            Red_dot4.SetActive(false); Blue_dot4.SetActive(false);
        }
        else
        {
            Red_dot1.SetActive(false); Blue_dot1.SetActive(false);
            Red_dot2.SetActive(false); Blue_dot2.SetActive(false);
            Red_dot3.SetActive(false); Blue_dot3.SetActive(false);
            Red_dot4.SetActive(true); Blue_dot4.SetActive(true);
        }
    }

    public void OnClickLeft()                                    //���� ����Ű ������ ��
    {
        if(!Mist_L.activeSelf)                                  //Reset ��ư ������ leftCount �ʱ�ȭ
        {
            leftCount = 0;
        }
        Mist_L?.SetActive(true);                               //Reset ���� ��Ȱ��ȭ
        leftCount++;                                           //leftCount ���� ����

        if (Red_dot1.activeSelf && Blue_dot1.activeSelf)
        {
            if (leftCount == 1)                                    //ù ��° ����(�������� �� ĭ) �ϼ�
            {
                First?.SetActive(false);
            }
            if (leftCount == 2 && Second.activeSelf)                //�ι�° ������(���� �� ĭ) �ϼ����� �ʾҴµ� ���� ��ư Ŭ���� ����ó��
            {
                Wrong?.SetActive(false);
            }
            if (leftCount == 2 && !Second.activeSelf)              //����° ���� (�������� �� ĭ) �ϼ�
            {
                Third?.SetActive(false);
            }
            if (leftCount == 3 && Fourth.activeSelf)                //�׹�° ����(�Ʒ��� �� ĭ ) �ϼ����� �ʾҴµ� ���� ��ư Ŭ���� ����ó��
            {
                Wrong?.SetActive(false);
            }
            if (leftCount == 4)                                    //�ټ���° ���� (�������� �� ĭ) �ϼ�
            {
                Fifth?.SetActive(false);
            }
            CheckSequence();                                    //���� �ϼ��ߴ��� üũ
        }

        if(Red_dot2.activeSelf && Blue_dot2.activeSelf)
        {
            if(leftCount ==1)
            {
                First?.SetActive(false);
            }
            if(Second.activeSelf && leftCount == 2)
            {
                Wrong?.SetActive(false);
            }
            if(!Second.activeSelf && leftCount == 2)
            {
                Third?.SetActive(false);
            }
            if(Fourth.activeSelf && leftCount == 3)
            {
                Wrong?.SetActive(false);
            }
            if(!Fourth.activeSelf && leftCount == 5)
            {
                Fifth?.SetActive(false);
            }
            if(leftCount == 6)
            {
                Wrong?.SetActive(false);
            }
            CheckSequence();
        }

        if(Red_dot3.activeSelf && Blue_dot3.activeSelf)
        {
            if(First.activeSelf)
            {
                Wrong?.SetActive(false);
            }
            if(!First.activeSelf && leftCount == 1)
            {
                Second?.SetActive(false);
            }
            if(Third.activeSelf && leftCount == 2)
            {
                Wrong?.SetActive(false);
            }
            if (!Third.activeSelf && leftCount == 2)
            {
                Fourth?.SetActive(false);
            }
            if(leftCount == 3)
            {
                Wrong?.SetActive(false);
            }
            CheckSequence();
        }
        if (Red_dot4.activeSelf && Blue_dot4.activeSelf)
        {
            if (leftCount == 1)
            {
                First?.SetActive(false);
            }
            if (Second.activeSelf && leftCount == 2)
            {
                Wrong?.SetActive(false);
            }
            if (!Second.activeSelf && leftCount == 2)
            {
                Third?.SetActive(false);
            }
            if (Fourth.activeSelf && leftCount == 3)
            {
                Wrong?.SetActive(false);
            }
            if (!Fourth.activeSelf && leftCount == 4)
            {
                Fifth?.SetActive(false);
            }
            if (leftCount == 5)
            {
                Wrong?.SetActive(false);
            }
            CheckSequence();
        }
    }

    public void OnClickUp()                                  //���� ����Ű ������ ��
    {
        if (!Mist_U.activeSelf)                              //Reset ��ư ������ upCount �ʱ�ȭ
        {
            upCount = 0;
        }
        Mist_U?.SetActive(true);                               //Reset ���� ��Ȱ��ȭ
        upCount++;                                            //upCount ���� ����

        if (Red_dot1.activeSelf && Blue_dot1.activeSelf)
        {
            if (First.activeSelf)                                 //ù��° ������(�������� �� ĭ) �ϼ����� �ʾҴµ� ���� ��ư Ŭ���� ����ó��
            {
                Wrong?.SetActive(false);
            }
            if (!First.activeSelf && upCount == 4)               //�ι�° ���� (�������� �� ĭ) �ϼ�  
            {
                Second?.SetActive(false);
            }
            if (upCount == 5)                                     //���� ��ư�� 5�� �̻� Ŭ���� ����ó��
            {
                Wrong?.SetActive(false);
            }
            CheckSequence();                                    //���� �ϼ��ߴ��� üũ
        }

        if (Red_dot2.activeSelf && Blue_dot2.activeSelf)
        {
            if (upCount == 1)
            {
                Wrong?.SetActive(false);
            }
            CheckSequence();
        }
        if (Red_dot3.activeSelf && Blue_dot3.activeSelf)
        {
            if (upCount == 1)
            {
                Wrong?.SetActive(false);
            }
            CheckSequence();
        }
        if (Red_dot4.activeSelf && Blue_dot4.activeSelf)
        {
            if (First.activeSelf && upCount == 1)
            {
                Wrong?.SetActive(false);
            }
            if (!First.activeSelf && upCount == 1)
            {
                Second?.SetActive(false);
            }
            if (Third.activeSelf && upCount == 2)
            {
                Wrong?.SetActive(false);
            }
            if (!Third.activeSelf && upCount == 2)
            {
                Fourth?.SetActive(false);
            }
            if (upCount == 3)
            {
                Wrong?.SetActive(false);
            }
            CheckSequence();
        }
    }

    public void OnClickDown()
    {
        if (!Mist_D.activeSelf)                              //Reset ��ư ������ downCount �ʱ�ȭ
        {
            downCount = 0;
        }
        Mist_D?.SetActive(true);
        downCount++;

        if (Red_dot1.activeSelf && Blue_dot1.activeSelf)
        {

            if (First.activeSelf)                                 //ù��° ������(�������� �� ĭ) �ϼ����� �ʾҴµ� ���� ��ư Ŭ���� ����ó��
            {
                Wrong?.SetActive(false);
            }
            if (Second.activeSelf)                               //�ι�° ������(���� ��ĭ) �ϼ����� �ʾҴµ� ���� ��ư Ŭ���� ����ó��
            {
                Wrong?.SetActive(false);
            }
            if (Third.activeSelf)                               //����° ������(���� ��ĭ) �ϼ����� �ʾҴµ� ���� ��ư Ŭ���� ����ó��
            {
                Wrong?.SetActive(false);
            }
            if (downCount == 1)                                   //�׹�° ���� (�Ʒ��� ��ĭ) �ϼ�
            {
                Fourth?.SetActive(false);
            }
            if (downCount == 2)                                  //�Ʒ��� ��ư�� �ι� �̻� ������ ����ó��
            {
                Wrong?.SetActive(false);
            }
            CheckSequence();                                    //���� �ϼ��ߴ��� üũ
        }
        if(Red_dot2.activeSelf && Blue_dot2.activeSelf)
        {
            if(First.activeSelf && downCount == 1)
            {
                Wrong?.SetActive(false);
            }
            if(!First.activeSelf && downCount == 1)
            {
                Second?.SetActive(false);
            }
            if(Third.activeSelf && downCount==2)
            {
                Wrong?.SetActive(false);
            }
            if(!Third.activeSelf && downCount == 2)
            {
                Fourth?.SetActive(false);
            }
            if(downCount == 3)
            {
                Wrong?.SetActive(false);
            }
            CheckSequence();
        }

        if (Red_dot3.activeSelf && Blue_dot3.activeSelf)
        {
            if(downCount == 2)
            {
                First?.SetActive(false);
            }
            if(Second.activeSelf && downCount == 3)
            {
                Wrong?.SetActive(false);
            }
            if(!Second.activeSelf && downCount == 4)
            {
                Third?.SetActive(false);
            }
            if(Fourth.activeSelf && downCount == 5)
            {
                Wrong?.SetActive(false);
            }
            if(!Fourth.activeSelf && downCount == 5)
            {
                Fifth?.SetActive(false);
            }
            if(downCount == 6)
            {
                Wrong?.SetActive(false);
            }
            CheckSequence();
        }
        if (Red_dot4.activeSelf && Blue_dot4.activeSelf)
        {
            if (downCount == 1)
            {
                Wrong?.SetActive(false);
            }
            CheckSequence();
        }
    }
    public void OnClickRight()                              //������ ����Ű ������ ���� �������� ����ó��
    {
        if (!Mist_R.activeSelf)                              //Reset ��ư ������ upCount �ʱ�ȭ
        {
            rightCount = 0;
        }
        Mist_R?.SetActive(true);
        rightCount++;

        if (rightCount == 1)
        {
            Wrong?.SetActive(false);
        }
        CheckSequence();
    }

    public void OnClickReset()                               //Reset ������ ��
    {
        Mist_U?.SetActive(false);                           //upCount ���� �����ֱ� ����
        Mist_D?.SetActive(false);                          //downCount ���� �����ֱ� ����
        Mist_L?.SetActive(false);                          //leftCount ���� �����ֱ� ����
        Mist_R?.SetActive(false);                          //rightCount ���� �����ֱ� ����

        First?.SetActive(true);                            //ù��° ���� �����Ȳ ����
        Second?.SetActive(true);                           //�ι�° ���� �����Ȳ ����
        Third?.SetActive(true);                            //����° ���� �����Ȳ ����
        Fourth?.SetActive(true);                           //�׹�° ���� �����Ȳ ����
        Fifth?.SetActive(true);                            //�ټ���° ���� �����Ȳ ����

        Wrong?.SetActive(true);                            // ����ó�� ����
    }

    private bool isPuzzleSolved = false;                  //���� �ذ� �ʱⰪ�� false
    private void CheckSequence()
    {
       if(!First.activeSelf && !Second.activeSelf && !Third.activeSelf && !Fourth.activeSelf && !Fifth.activeSelf && Wrong.activeSelf)
       {
            isPuzzleSolved = true;                                           //��� ������ �����ϸ� ���� �ذᰪ�� true�� �ٲ�
       }
        else
        {
            isPuzzleSolved = false;
        }
    }
    public void OnclickSquareBUtton()                                       //Push ��ư ������ ��
    {
        CheckSequence();                                                    // isPuzzleSolved ���� true�� ���� �ذ�
        puzzleMainFrame.SetLightCoverUI(isPuzzleSolved);
    }
}