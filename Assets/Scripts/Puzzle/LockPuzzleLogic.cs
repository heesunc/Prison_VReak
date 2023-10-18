using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockPuzzleLogic : MonoBehaviour
{
    public float rotationAngle = 36.0f;
    public Button rotateButton;

    private int Remain1;
    private float clickCount1 = 0f;
    public GameObject LockImage1;

    private int Remain2;
    private float clickCount2 = 0f;
    public GameObject LockImage2;

    public PuzzleMainFrame puzzleMainFrame;


    public GameObject Image1;
    public GameObject image2;

    public GameObject image3;

    private void Start()
    {
        int randomValue = Random.Range(0, 4);

        if (randomValue == 0)
        {
            Image1.SetActive(true); // 활성화
            image2.SetActive(true);
        }
        else if(randomValue == 1)
        {
            Image1.SetActive(false); // 비활성화
            image2.SetActive(true);
        }
        else if(randomValue == 2)
        {
            Image1.SetActive(true);
            image2.SetActive(false);
        }
        else
        {
            Image1.SetActive(false);
            image2.SetActive(false);
        }


        int randomValue2 = Random.Range(0, 2);

        if(randomValue2 == 0)
        {
            image3.SetActive(true);
        }
        else
        {
            image3.SetActive(false);
        }
    }


    public void OnClickLock1Button()
    {
        transform.Rotate(Vector3.forward, rotationAngle);
        clickCount1++;
        Remain1 = (int)(clickCount1 % 10);

        //Debug.Log("클릭 카운트: " + clickCount1 + ", 나머지: " + Remain1);

        if(Image1.activeSelf && image2.activeSelf)
        {
            if(clickCount1 % 10 == 5)
            {
                LockImage1?.SetActive(false);
            }
            else
            {
                LockImage1?.SetActive(true);
            }
            CheckPuzzleSolved();
        }

        else if(!Image1.activeSelf && image2.activeSelf)
        {
            if(clickCount1 % 10 ==6)
            {
                LockImage1?.SetActive(false);
            }
            else
            {
                LockImage1?.SetActive(true);
            }
            CheckPuzzleSolved();
        }

        else if(Image1.activeSelf && !image2.activeSelf)
        {
            if (clickCount1 % 10 == 6)
            {
                LockImage1?.SetActive(false);
            }
            else
            {
                LockImage1?.SetActive(true);
            }
            CheckPuzzleSolved();
        }
        else
        {
            if (clickCount1 % 10 == 7)
            {
                LockImage1?.SetActive(false);
            }
            else
            {
                LockImage1?.SetActive(true);
            }
            CheckPuzzleSolved();
        }
        
    }

    public void OnClickLock2Button()
    {
        transform.Rotate(Vector3.forward, rotationAngle);
        clickCount2++;
        Remain2 = (int)(clickCount2 % 10);

        //Debug.Log("클릭 카운트: " + clickCount2 + ", 나머지: " + Remain2);

        if(image3.activeSelf)
        {
            if(clickCount2 % 10 == 4)
            {
                LockImage2?.SetActive(false);
            }
            else
            {
                LockImage2?.SetActive(true);
            }
            CheckPuzzleSolved();
        }

        else
        {
            if (clickCount2 % 10 == 5)
            {
                LockImage2?.SetActive(false);
            }
            else
            {
                LockImage2?.SetActive(true);
            }
            CheckPuzzleSolved();
        }
    }


    private bool isPuzzleSolved = false;
    private void CheckPuzzleSolved()
    {
        if(!LockImage1.activeSelf && !LockImage2.activeSelf)
        {
            isPuzzleSolved = true;
        }
        else
        {
            isPuzzleSolved = false;
        }
    }

    public void OnClickSquareButton()
    {
        CheckPuzzleSolved();                                               // 퍼즐 상태 체크
        PuzzleManager.instance.PuzzleJudge(isPuzzleSolved);
    }


}