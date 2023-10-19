using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorsePuzzleLogic : MonoBehaviour                                     //변수 설정
{
    public GameObject square;                                          //해당 버튼(세모난 모양의 버튼)의 바로 밑에 자리하고 있는 회색 네모모양의 UI
    public GameObject dot;                                             //회색 네모모양의 UI 위에 자리하고 있는 점 UI
    public GameObject square1;                                         //7번 줄에서 설명한 회색 네모 UI들 중 왼쪽에서 첫번째의 것
    public GameObject square2;                                         //7번 줄에서 설명한 회색 네모 UI들 중 왼쪽에서 두번째의 것
    public GameObject square3;                                         //7번 줄에서 설명한 회색 네모 UI들 중 왼쪽에서 세번째의 것
    public GameObject square4;                                         //7번 줄에서 설명한 회색 네모 UI들 중 왼쪽에서 네번째의 것
    public GameObject square5;                                         //7번 줄에서 설명한 회색 네모 UI들 중 왼쪽에서 다섯번째의 것
    public GameObject square6;                                         //7번 줄에서 설명한 회색 네모 UI들 중 왼쪽에서 여섯번째의 것                                     //초록 불을 가리고 있는 검은색 UI

    public PuzzleMainFrame puzzleMainFrame;

    public void OnClickSquare1()                                      //UI를 토글하고 퍼즐 상태를 업데이트 하는 함수
    {
        ToggleUI(square1);
        ToggleUI(dot);
        CheckPuzzleSolved();
    }

    public void OnClickSquare2()                                      //UI를 토글하고 퍼즐 상태를 업데이트 하는 함수
    {
        ToggleUI(square2);
        ToggleUI(dot);
        CheckPuzzleSolved();
    }

    public void OnClickSquare3()                                      //UI를 토글하고 퍼즐 상태를 업데이트 하는 함수
    {
        ToggleUI(square3);
        ToggleUI(dot);
        CheckPuzzleSolved();
    }

    public void OnClickSquare4()                                      //UI를 토글하고 퍼즐 상태를 업데이트 하는 함수
    {
        ToggleUI(square4);
        ToggleUI(dot);
        CheckPuzzleSolved();
    }

    public void OnClickSquare5()                                      //UI를 토글하고 퍼즐 상태를 업데이트 하는 함수
    {
        ToggleUI(square5);
        ToggleUI(dot);
        CheckPuzzleSolved();
    }

    public void OnClickSquare6()                                      //UI를 토글하고 퍼즐 상태를 업데이트 하는 함수
    {
        ToggleUI(square6);
        ToggleUI(dot);
        CheckPuzzleSolved();
    }

    private void ToggleUI(GameObject uiObject)                        //UI 토글하는 함수 생성
    {
        bool isUIVisible = !uiObject.activeSelf;
        uiObject.SetActive(isUIVisible);
    }

    private bool isPuzzleSolved = false;                            // 퍼즐이 해결되었는지 여부를 저장하는 변수. 초기 값은 false로 설정

    private void CheckPuzzleSolved()
    {
        if (!square1.activeSelf && !square2.activeSelf && !square3.activeSelf && !square5.activeSelf && square4.activeSelf && square6.activeSelf)
        {
            isPuzzleSolved = true;                                    // 퍼즐이 해결되었음을 표시
        }
        else
        {
            isPuzzleSolved = false;                                   // 퍼즐이 해결되지 않았음을 표시
        }
    }

    public void OnClickSquareButton()
    {
        CheckPuzzleSolved();                                               // 퍼즐 상태 체크
        PuzzleManager.instance.PuzzleJudge(isPuzzleSolved);
    }
}