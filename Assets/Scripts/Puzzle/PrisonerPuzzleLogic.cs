using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrisonerPuzzleLogic : MonoBehaviour
{
    public GameObject photo;                                          //해당 폴라로이드 사진
    public GameObject photo1;                                         //Steve 사진
    public GameObject photo2;                                         //Logan 사진
    public GameObject photo3;                                         //Kim 사진
    public GameObject photo4;                                         //Cindy 사진
    public GameObject photo5;                                         //Jack 사진
    public GameObject photo6;                                         //Rachel 사진

    public PuzzleMainFrame puzzleMainFrame;                                   //PUSH가 적혀있는 네모난 모양의 버튼 UI

    public void OnClickPhoto1()                                      //UI를 토글하고 퍼즐 상태를 업데이트 하는 함수
    {
        ToggleUI(photo1);
        CheckPuzzleSolved();
    }

    public void OnClickPhoto2()                                      //UI를 토글하고 퍼즐 상태를 업데이트 하는 함수
    {
        ToggleUI(photo2);
        CheckPuzzleSolved();
    }

    public void OnClickPhoto3()                                      //UI를 토글하고 퍼즐 상태를 업데이트 하는 함수
    {
        ToggleUI(photo3);
        CheckPuzzleSolved();
    }

    public void OnClickPhoto4()                                      //UI를 토글하고 퍼즐 상태를 업데이트 하는 함수
    {
        ToggleUI(photo4);
        CheckPuzzleSolved();
    }

    public void OnClickPhoto5()                                      //UI를 토글하고 퍼즐 상태를 업데이트 하는 함수
    {
        ToggleUI(photo5);
        CheckPuzzleSolved();
    }

    public void OnClickPhoto6()                                      //UI를 토글하고 퍼즐 상태를 업데이트 하는 함수
    {
        ToggleUI(photo6);
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
        if (!photo5.activeSelf && !photo6.activeSelf && photo1.activeSelf && photo2.activeSelf && photo3.activeSelf && photo4.activeSelf)
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
        puzzleMainFrame.SetLightCoverUI(isPuzzleSolved);
    }
}