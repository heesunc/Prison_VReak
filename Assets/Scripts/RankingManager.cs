using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class RankingManager : MonoBehaviour
{
    public GameObject rankCanavs;
    public GameObject keyGuideCanavs;
    public TMP_Text rankText;
    UserInfo userInfo;

    private string getRankingListUrl = "https://prisonvreak.store/";


    private void Awake()
    {
        UserInfoContainer userInfoData = FindObjectOfType<UserInfoContainer>();
        if(userInfoData != null)
        {
            userInfo = userInfoData.GetUserInfo();

            // 이 부분은 따로 메소드로 빼야 될 듯. 웹에서 가져온 랭킹 데이터도 formating 필요하니까
            long clearTimeLong = long.Parse(userInfo.clearTime);
            TimeSpan t = TimeSpan.FromMilliseconds(clearTimeLong);
            string rankData = string.Format("[ Clear Time: {0:D2}:{1:D2}.{2:D3} ]",t.Minutes,t.Seconds,t.Milliseconds);
            ////////////------------------------------////////////
            
            keyGuideCanavs.SetActive(false);
            rankCanavs.SetActive(true);
            rankText.SetText(rankData);
        }
    }

    // ------------WIP--------------------- //
    public void RankingListProc()
    {
        StartCoroutine(GetRankingListRequest(userInfo.clearTime));
    }

    private IEnumerator GetRankingListRequest(string clearTime)
    {
        WWWForm form = new WWWForm();
        form.AddField("필드 이름 넣기", clearTime);

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
                Debug.Log(responseText);

                if (responseText.Equals("로그인 정보가 일치하지 않습니다."))
                {
                    //OpenMessageWindow("로그인 정보가 일치하지 않습니다.", loginCanvas);
                }
                else if (responseText.Equals("로그인 성공"))
                {
                    // 데이터 받아서 ui로 출력해주는 부분
                }
                else
                {
                    //OpenMessageWindow("알 수 없는 예외:LR2", loginCanvas);
                }
            }
        }
    }

    // ------------------------------------------ //
}
