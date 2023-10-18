using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    public GameObject rankCanavs;
    public GameObject keyGuideCanavs;
    public TMP_Text rankText;


    private void Awake()
    {
        UserInfoContainer userInfoData = FindObjectOfType<UserInfoContainer>();
        if(userInfoData != null)
        {
            UserInfo userInfo = userInfoData.GetUserInfo();
            string rankData = "VR: [" + userInfo.vr_userCode 
                + "] / Web: [" + userInfo.web_userCode 
                + "] / Time: " + userInfo.clearTime + "]";
            keyGuideCanavs.SetActive(false);
            rankCanavs.SetActive(true);
            rankText.SetText(rankData);
        }
    }

}
