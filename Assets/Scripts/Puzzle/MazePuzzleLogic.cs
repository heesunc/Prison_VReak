using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazePuzzleLogic : MonoBehaviour
{
    public int upCount = 0;                                         //화살표 클릭 횟수 숫자 카운트
    public int downCount = 0;
    public int rightCount = 0;
    public int leftCount = 0;

    public GameObject First;                                       //방향 바뀌는 조건 설정
    public GameObject Second;
    public GameObject Third;
    public GameObject Fourth;
    public GameObject Fifth;

    public GameObject Wrong;                                      //조건과 맞지 않는 버튼 클릭 시 오답 처리

    public GameObject Mist_U;                                     //Reset 버튼 누를 시 초기화 해주기 위함
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

    public void OnClickLeft()                                    //왼쪽 방향키 눌렀을 때
    {
        if(!Mist_L.activeSelf)                                  //Reset 버튼 누르면 leftCount 초기화
        {
            leftCount = 0;
        }
        Mist_L?.SetActive(true);                               //Reset 설정 비활성화
        leftCount++;                                           //leftCount 숫자 증가

        if (Red_dot1.activeSelf && Blue_dot1.activeSelf)
        {
            if (leftCount == 1)                                    //첫 번째 조건(왼쪽으로 한 칸) 완성
            {
                First?.SetActive(false);
            }
            if (leftCount == 2 && Second.activeSelf)                //두번째 조건을(위로 네 칸) 완성하지 않았는데 왼쪽 버튼 클릭시 오답처리
            {
                Wrong?.SetActive(false);
            }
            if (leftCount == 2 && !Second.activeSelf)              //세번째 조건 (왼쪽으로 한 칸) 완성
            {
                Third?.SetActive(false);
            }
            if (leftCount == 3 && Fourth.activeSelf)                //네번째 조건(아래로 한 칸 ) 완성하지 않았는데 왼쪽 버튼 클릭시 오답처리
            {
                Wrong?.SetActive(false);
            }
            if (leftCount == 4)                                    //다섯번째 조건 (왼쪽으로 두 칸) 완성
            {
                Fifth?.SetActive(false);
            }
            CheckSequence();                                    //퍼즐 완성했는지 체크
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

    public void OnClickUp()                                  //위쪽 방향키 눌렀을 때
    {
        if (!Mist_U.activeSelf)                              //Reset 버튼 누르면 upCount 초기화
        {
            upCount = 0;
        }
        Mist_U?.SetActive(true);                               //Reset 설정 비활성화
        upCount++;                                            //upCount 숫자 증가

        if (Red_dot1.activeSelf && Blue_dot1.activeSelf)
        {
            if (First.activeSelf)                                 //첫번째 조건을(왼쪽으로 한 칸) 완성하지 않았는데 위쪽 버튼 클릭시 오답처리
            {
                Wrong?.SetActive(false);
            }
            if (!First.activeSelf && upCount == 4)               //두번째 조건 (위쪽으로 네 칸) 완성  
            {
                Second?.SetActive(false);
            }
            if (upCount == 5)                                     //위쪽 버튼을 5번 이상 클릭시 오답처리
            {
                Wrong?.SetActive(false);
            }
            CheckSequence();                                    //퍼즐 완성했는지 체크
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
        if (!Mist_D.activeSelf)                              //Reset 버튼 누르면 downCount 초기화
        {
            downCount = 0;
        }
        Mist_D?.SetActive(true);
        downCount++;

        if (Red_dot1.activeSelf && Blue_dot1.activeSelf)
        {

            if (First.activeSelf)                                 //첫번째 조건을(왼쪽으로 한 칸) 완성하지 않았는데 위쪽 버튼 클릭시 오답처리
            {
                Wrong?.SetActive(false);
            }
            if (Second.activeSelf)                               //두번째 조건을(위로 네칸) 완성하지 않았는데 왼쪽 버튼 클릭시 오답처리
            {
                Wrong?.SetActive(false);
            }
            if (Third.activeSelf)                               //세번째 조건을(왼쪽 한칸) 완성하지 않았는데 왼쪽 버튼 클릭시 오답처리
            {
                Wrong?.SetActive(false);
            }
            if (downCount == 1)                                   //네번째 조건 (아래로 한칸) 완성
            {
                Fourth?.SetActive(false);
            }
            if (downCount == 2)                                  //아래쪽 버튼을 두번 이상 누르면 오답처리
            {
                Wrong?.SetActive(false);
            }
            CheckSequence();                                    //퍼즐 완성했는지 체크
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
    public void OnClickRight()                              //오른쪽 방향키 눌렀을 때는 언제든지 오답처리
    {
        if (!Mist_R.activeSelf)                              //Reset 버튼 누르면 upCount 초기화
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

    public void OnClickReset()                               //Reset 눌렀을 때
    {
        Mist_U?.SetActive(false);                           //upCount 리셋 시켜주기 위함
        Mist_D?.SetActive(false);                          //downCount 리셋 시켜주기 위함
        Mist_L?.SetActive(false);                          //leftCount 리셋 시켜주기 위함
        Mist_R?.SetActive(false);                          //rightCount 리셋 시켜주기 위함

        First?.SetActive(true);                            //첫번째 조건 진행상황 리셋
        Second?.SetActive(true);                           //두번째 조건 진행상황 리셋
        Third?.SetActive(true);                            //세번째 조건 진행상황 리셋
        Fourth?.SetActive(true);                           //네번째 조건 진행상황 리셋
        Fifth?.SetActive(true);                            //다섯번째 조건 진행상황 리셋

        Wrong?.SetActive(true);                            // 오답처리 리셋
    }

    private bool isPuzzleSolved = false;                  //퍼즐 해결 초기값은 false
    private void CheckSequence()
    {
       if(!First.activeSelf && !Second.activeSelf && !Third.activeSelf && !Fourth.activeSelf && !Fifth.activeSelf && Wrong.activeSelf)
       {
            isPuzzleSolved = true;                                           //모든 조건을 만족하면 퍼즐 해결값을 true로 바꿈
       }
        else
        {
            isPuzzleSolved = false;
        }
    }
    public void OnclickSquareBUtton()                                       //Push 버튼 눌렀을 때
    {
        CheckSequence();                                                    // isPuzzleSolved 값이 true면 퍼즐 해결
        PuzzleManager.instance.PuzzleJudge(isPuzzleSolved);
    }
}