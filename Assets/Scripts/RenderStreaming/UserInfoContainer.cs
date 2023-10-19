using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfoContainer : MonoBehaviour
{
    private UserInfo userInfo;

    public void SetUserInfo(UserInfo info)
    {
        userInfo = info;
    }

    public UserInfo GetUserInfo()
    {
        return userInfo;
    }

    public void SetClearTime(string clearTime)
    {
        userInfo.clearTime = clearTime;
    }
}