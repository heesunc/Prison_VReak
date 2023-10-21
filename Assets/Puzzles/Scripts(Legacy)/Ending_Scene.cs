using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;
using TMPro;

public class Ending_Scene : MonoBehaviour
{
    public SceneTransitionManager sceneController;

    public GameObject rankBackgroundCanvas;
    public GameObject webRankBackgroundCanvas;
    public GameObject rankFieldPrefab; // RankField 프리팹
    public GameObject vrFieldPrefab; // VRField 프리팹
    public GameObject webFieldPrefab; // WEBField 프리팹
    public GameObject timeFieldPrefab; // TimeField 프리팹
    public GameObject myRankFieldPrefab; // RankField 프리팹
    public GameObject myVrFieldPrefab; // VRField 프리팹
    public GameObject myWebFieldPrefab; // WEBField 프리팹
    public GameObject myTimeFieldPrefab; // TimeField 프리팹
    public float yOffset; // 랭킹 항목 간격
    UserInfoContainer userInfoContainer;
    private string createRankURL = "https://prisonvreak.store/createRank";
    private string getRankingListUrl = "https://prisonvreak.store/vrGetAfterClearRank";
    private string web_userCode;
    private string vr_userCode;
    bool isTriggerEntered = false;
    public NpcTest npcTest;
    public GameObject clearCanvas;
    public GameObject webClearCanvas;
    public GameObject emergencyClearCanvas;
    public TMP_Text clearTimeText;

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

            if(GameManager.Instance.isEmergency)
            {
                npcTest.GameOverProc(emergencyClearCanvas, webClearCanvas);
                clearTimeText.text = MillisecondsStringToTimeFormat(totalMilliseconds);
            }
            else
            {
                StartCoroutine(RankInsertRequest(web_userCode, vr_userCode, totalMilliseconds));
            }

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
                    StartCoroutine(GetAfterClearRankingListRequest(clearTime));
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
    public void OnMainButtonClicked()
    {
        sceneController.GoToScene(0);
    }

    private IEnumerator GetAfterClearRankingListRequest(string clearTime){

        WWWForm form = new WWWForm();
        form.AddField("clearTime", clearTime);
        string formattedTime = MillisecondsStringToTimeFormat(clearTime);

        using (UnityWebRequest www = UnityWebRequest.Post(getRankingListUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("랭킹 데이터 요청 실패: " + www.error);
                //OpenMessageWindow("랭킹 데이터 요청 실패: " + www.error, loginCanvas);
            }
            else
            {
                string responseText = www.downloadHandler.text;
                
                List<RankData> rankDataList = JsonUtility.FromJson<RankDataList>("{\"rankingData\":" + responseText + "}").rankingData;

                float currentYPosition = 25f; // 초기 Y 위치

                Debug.Log(rankDataList);

                // 랭킹 아이템 UI 동적 생성
                foreach (RankData data in rankDataList)
                {
                    GameObject web_rankField;
                    GameObject web_vrField;
                    GameObject web_webField;
                    GameObject web_timeField;

                    GameObject rankField;
                    GameObject vrField;
                    GameObject webField;
                    GameObject timeField;

                    if (data.formatted_game_clear_time == formattedTime)
                    {

                        // (웹이 보는 화면)필드 프리팹을 복제
                        web_rankField = Instantiate(myRankFieldPrefab, webRankBackgroundCanvas.transform);
                        web_vrField = Instantiate(myVrFieldPrefab, webRankBackgroundCanvas.transform);
                        web_webField = Instantiate(myWebFieldPrefab, webRankBackgroundCanvas.transform);
                        web_timeField = Instantiate(myTimeFieldPrefab, webRankBackgroundCanvas.transform);

                        // 필드 프리팹을 복제
                        rankField = Instantiate(myRankFieldPrefab, rankBackgroundCanvas.transform);
                        vrField = Instantiate(myVrFieldPrefab, rankBackgroundCanvas.transform);
                        webField = Instantiate(myWebFieldPrefab, rankBackgroundCanvas.transform);
                        timeField = Instantiate(myTimeFieldPrefab, rankBackgroundCanvas.transform);
                    }
                    else
                    {
                        // (웹이 보는 화면)필드 프리팹을 복제
                        web_rankField = Instantiate(rankFieldPrefab, webRankBackgroundCanvas.transform);
                        web_vrField = Instantiate(vrFieldPrefab, webRankBackgroundCanvas.transform);
                        web_webField = Instantiate(webFieldPrefab, webRankBackgroundCanvas.transform);
                        web_timeField = Instantiate(timeFieldPrefab, webRankBackgroundCanvas.transform);

                        // 필드 프리팹을 복제
                        rankField = Instantiate(rankFieldPrefab, rankBackgroundCanvas.transform);
                        vrField = Instantiate(vrFieldPrefab, rankBackgroundCanvas.transform);
                        webField = Instantiate(webFieldPrefab, rankBackgroundCanvas.transform);
                        timeField = Instantiate(timeFieldPrefab, rankBackgroundCanvas.transform);
                    }

                    // (웹이 보는 화면)필요한 위치와 내용 설정
                    SetFieldText(web_rankField, data.rank);
                    SetFieldText(web_vrField, data.vr_nickname);
                    SetFieldText(web_webField, data.web_nickname);
                    SetFieldText(web_timeField, data.formatted_game_clear_time);
                    // 필요한 위치와 내용 설정
                    SetFieldText(rankField, data.rank);
                    SetFieldText(vrField, data.vr_nickname);
                    SetFieldText(webField, data.web_nickname);
                    SetFieldText(timeField, data.formatted_game_clear_time);

                    // (웹이 보는 화면)위치 조정
                    web_rankField.transform.localPosition = new Vector3(-170f, currentYPosition, 0);
                    web_vrField.transform.localPosition = new Vector3(-60f, currentYPosition, 0);
                    web_webField.transform.localPosition = new Vector3(60f, currentYPosition, 0);
                    web_timeField.transform.localPosition = new Vector3(170f, currentYPosition, 0);

                    // 위치 조정
                    rankField.transform.localPosition = new Vector3(-170f, currentYPosition, 0);
                    vrField.transform.localPosition = new Vector3(-60f, currentYPosition, 0);
                    webField.transform.localPosition = new Vector3(60f, currentYPosition, 0);
                    timeField.transform.localPosition = new Vector3(170f, currentYPosition, 0);

                    // 다음 항목의 위치 조정
                    currentYPosition -= yOffset; // 다음 항목의 위치를 조절하여 겹치지 않게
                    
                }
            }
        }
    }

     // 프리팹 내의 Text 엘리먼트에 텍스트 설정
    void SetFieldText(GameObject fieldPrefab, string text)
    {
        TMP_Text textField = fieldPrefab.GetComponent<TMP_Text>();
        textField.text = text;
    }

    public string MillisecondsStringToTimeFormat(string millisecondsString)
{
    if (int.TryParse(millisecondsString, out int milliseconds))
    {
        // 밀리초를 초, 분, 밀리초로 변환
        int seconds = Mathf.FloorToInt(milliseconds / 1000);
        int minutes = seconds / 60;
        int remainingSeconds = seconds % 60;
        int remainingMilliseconds = Mathf.FloorToInt(milliseconds % 1000);

        // 문자열 형태로 포맷팅
        string timeFormat = string.Format("{0:00}:{1:00}.{2:000}", minutes, remainingSeconds, remainingMilliseconds);

        return timeFormat;
    }
    else
    {
        // 밀리초가 숫자로 변환할 수 없는 경우 예외 처리
        return "Invalid Milliseconds";
    }
}
}
