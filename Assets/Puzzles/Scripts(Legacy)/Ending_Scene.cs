using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;

public class Ending_Scene : MonoBehaviour
{
    public SceneTransitionManager sceneController;

    UserInfoContainer userInfoContainer;
    private string createRankURL = "https://prisonvreak.store/createRank";
    private string web_userCode;
    private string vr_userCode;
    bool isTriggerEntered = false;
    public NpcTest npcTest;
    public GameObject clearCanvas;
    public GameObject webClearCanvas;

    private void Awake()
    {
        userInfoContainer = FindObjectOfType<UserInfoContainer>();
        if (userInfoContainer != null)
        {
            UserInfo loadedUserInfo = userInfoContainer.GetUserInfo();

            // 필요한 값 사용
            vr_userCode = loadedUserInfo.vr_userCode;
            web_userCode = loadedUserInfo.web_userCode;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggerEntered)
        {
            isTriggerEntered = true;

            // 탈출구 도착 시간 저장
            DateTime endTime = DateTime.Now;

            // 시작 시간 가져오기
            DateTime startTime = GameManager.Instance.GetStartTime();

            // 플레이 타임 계산
            TimeSpan clearTime = endTime - startTime;

            string totalMilliseconds = Math.Floor(clearTime.TotalMilliseconds).ToString();

            StartCoroutine(RankInsertRequest(web_userCode, vr_userCode, totalMilliseconds));

            Debug.Log("플레이 타임: " + totalMilliseconds);
        }

    }

    private IEnumerator RankInsertRequest(string web_userCode, string vr_userCode, string clearTime)
    {
        WWWForm form = new WWWForm();
        form.AddField("web_userCode", web_userCode);
        form.AddField("vr_userCode", vr_userCode);
        form.AddField("cleartime", clearTime);

        using (UnityWebRequest www = UnityWebRequest.Post(createRankURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("랭킹 등록 실패: " + www.error);
            }
            else
            {
                string responseText = www.downloadHandler.text;
                Debug.Log(responseText);

                if (responseText.Equals("등록 완료"))
                {
                    userInfoContainer.SetClearTime(clearTime);
                    npcTest.GameOverProc(clearCanvas, webClearCanvas);
                    Debug.Log("탈출!!");
                    // TODO 로딩 창 출력(1초 이하), 로딩 후 랭킹 정보 뜨게
                }
                else if (responseText.Equals("정상적인 방이 아닙니다"))
                {
                    Debug.Log("랭킹등록 오류");
                }
                else
                {
                    Debug.Log("알 수 없는 예외");
                }
            }
        }
    }
}
