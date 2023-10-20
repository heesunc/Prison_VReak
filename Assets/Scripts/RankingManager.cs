using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class RankingManager : MonoBehaviour
{
    public GameObject rankBackgroundCanvas;
    public GameObject rankFieldPrefab; // RankField 프리팹
    public GameObject vrFieldPrefab; // VRField 프리팹
    public GameObject webFieldPrefab; // WEBField 프리팹
    public GameObject timeFieldPrefab; // TimeField 프리팹
    public float yOffset; // 랭킹 항목 간격
    private List<GameObject> prefabInstances = new List<GameObject>();
    private string getAllRankingListUrl = "https://prisonvreak.store/vrGetAllRank";
    

    public void AllRankingListProc()
    {
        StartCoroutine(GetAllRankingListRequest());
    }

    private IEnumerator GetAllRankingListRequest(){

        using (UnityWebRequest www = UnityWebRequest.Get(getAllRankingListUrl))
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

                float currentYPosition = 85f; // 초기 Y 위치

                // 랭킹 아이템 UI 동적 생성
                foreach (RankData data in rankDataList)
                {
                    // 필드 프리팹을 복제
                    GameObject rankField = Instantiate(rankFieldPrefab, rankBackgroundCanvas.transform);
                    GameObject vrField = Instantiate(vrFieldPrefab, rankBackgroundCanvas.transform);
                    GameObject webField = Instantiate(webFieldPrefab, rankBackgroundCanvas.transform);
                    GameObject timeField = Instantiate(timeFieldPrefab, rankBackgroundCanvas.transform);
                    prefabInstances.Add(rankField); // 리스트에 추가
                    prefabInstances.Add(vrField); // 리스트에 추가
                    prefabInstances.Add(webField); // 리스트에 추가
                    prefabInstances.Add(timeField); // 리스트에 추가

                    // 필요한 위치와 내용 설정
                    SetFieldText(rankField, data.rank);
                    SetFieldText(vrField, data.vr_nickname);
                    SetFieldText(webField, data.web_nickname);
                    SetFieldText(timeField, data.formatted_game_clear_time);

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

//  ----------------------------- WIP -------------------------------  //

    // ------------------------------------------ //

    // 캔버스를 나갈 때 프리팹 오브젝트 삭제
    public void DestroyPrefabInstances()
    {
        foreach (var instance in prefabInstances)
        {
            Destroy(instance); // 프리팹 인스턴스 삭제
        }
        prefabInstances.Clear(); // 리스트 비우기
    }

     // 프리팹 내의 Text 엘리먼트에 텍스트 설정
    void SetFieldText(GameObject fieldPrefab, string text)
    {
        TMP_Text textField = fieldPrefab.GetComponent<TMP_Text>();
        textField.text = text;
    }
}
