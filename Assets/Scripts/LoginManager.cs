using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit.UI;
using System;

public class LoginManager : MonoBehaviour
{
    [SerializeField] public TMP_InputField InputField_UserCode;
    [SerializeField] public TMP_InputField InputField_PW;
    [SerializeField] public TMP_InputField InputField_PartnerUserCode;
    public SceneTransitionManager sceneController;
    public GameObject loginCanvas;
    public GameObject msgCanvas;
    public TMP_Text msgText;
    public GameObject matchingCanvas;
    public GameObject loadingCanvas;

    private string loginUrl = "https://prisonvreak.store/auth/vr_login_process"; // 로그인 처리를 수행하는 서버 스크립트의 URL을 지정해야 합니다.
    private string createOrJoinUrl = "https://prisonvreak.store/vrCreateOrJoinRoom";
    private string matchingUrl = "https://prisonvreak.store/checkMatching";
    

    public void OnLoginButtonClicked()
    {
        string userCode = InputField_UserCode.text;
        string pwd = InputField_PW.text;

        StartCoroutine(LoginRequest(userCode, pwd));
    }
    public void OnMatchingButtonClicked()
    {
        string userCode = InputField_UserCode.text;
        string p_userCode = InputField_PartnerUserCode.text;

        StartCoroutine(MatchingRequest(p_userCode, userCode));
    }

    public void OpenMessageWindow(string msg, GameObject beforeCanvas)
    {
        TrackedDeviceGraphicRaycaster beforeCanvasRaycaster = beforeCanvas.GetComponent<TrackedDeviceGraphicRaycaster>();
        beforeCanvasRaycaster.enabled = false;
        msgCanvas.SetActive(true);
        msgText.SetText(msg);
    }

    private void OpenLoadingWindow(GameObject beforeCanvas)
    {
        TrackedDeviceGraphicRaycaster beforeCanvasRaycaster = beforeCanvas.GetComponent<TrackedDeviceGraphicRaycaster>();
        beforeCanvasRaycaster.enabled = false;
        loadingCanvas.SetActive(true);
    }

    private void CloseLoadingWindow()
    {
        loadingCanvas.SetActive(false);
    }

    private IEnumerator LoginRequest(string userCode, string pwd)
    {
        WWWForm form = new WWWForm();
        form.AddField("userCode", userCode);
        form.AddField("pwd", pwd);

        using (UnityWebRequest www = UnityWebRequest.Post(loginUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("로그인 요청 실패: " + www.error);
                OpenMessageWindow("로그인 요청 실패: " + www.error, loginCanvas);
            }
            else
            {
                string responseText = www.downloadHandler.text;
                UserInfo userInfo = JsonUtility.FromJson<UserInfo>(responseText);
                Debug.Log(responseText);

                if (userInfo.message.Equals("로그인 정보가 일치하지 않습니다."))
                {
                    Debug.Log("로그인 정보가 일치하지 않습니다.");
                    OpenMessageWindow("로그인 정보가 일치하지 않습니다.", loginCanvas);
                }
                // 추후 이 부분 제거, 유니티에서 자체적으로 빈칸 검증하게끔/////////////////
                else if (userInfo.message.Equals("아이디와 비밀번호를 입력하세요."))
                {
                    Debug.Log("아이디와 비밀번호를 입력하세요.");
                    OpenMessageWindow("아이디와 비밀번호를 입력하세요.", loginCanvas);
                }
                //////////////////////////////////////////
                else if(userInfo.message.Equals("로그인 성공"))
                {
                    

                    // 로그인 후 받아온 정보를 다음 씬에 전달
                    GameObject userInfoObject = new GameObject("UserInfoObject");
                    DontDestroyOnLoad(userInfoObject);
                    userInfoObject.AddComponent<UserInfoContainer>().SetUserInfo(userInfo);

                    Debug.Log("로그인 성공");

                    loginCanvas.SetActive(false);
                    matchingCanvas.SetActive(true);

                }
                else
                {
                    Debug.Log("알 수 없는 예외");
                    OpenMessageWindow("알 수 없는 예외:LR2", loginCanvas);
                }
            }
        }
    }

    private IEnumerator MatchingRequest(string p_userCode, string userCode)
    {
        WWWForm form = new WWWForm();
        form.AddField("web_userCode", p_userCode);
        form.AddField("vr_userCode", userCode);

        using (UnityWebRequest www = UnityWebRequest.Post(createOrJoinUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("매칭 실패: " + www.error);
                OpenMessageWindow("매칭 실패: " + www.error, matchingCanvas);
            }
            else
            {
                string responseText = www.downloadHandler.text;
                Debug.Log(responseText);

                if (responseText.Equals("존재하지 않는 유저코드입니다."))
                {
                    Debug.Log("로그인 정보가 일치하지 않습니다.");
                    OpenMessageWindow("존재하지 않는 유저코드입니다.", matchingCanvas);
                }
                // 추후 이 부분 제거, 유니티에서 자체적으로 빈칸 검증하게끔////////////////
                else if (responseText.Equals("파트너 코드를 입력하세요."))
                {
                    Debug.Log("파트너 코드를 입력하세요.");
                    OpenMessageWindow("파트너 코드를 입력하세요.", matchingCanvas);
                }
                ////////////////////////////////////////////////////////
                else if(responseText.Equals("유저코드 유효성 검사 완료"))
                {
                    OpenLoadingWindow(matchingCanvas);
                    StartCoroutine(RealMatchingRequest(p_userCode, userCode));
                }
                else{
                    Debug.Log("알 수 없는 예외");
                    OpenMessageWindow("알 수 없는 예외:MR2", matchingCanvas);
                }
            }
        }

    }

    private IEnumerator RealMatchingRequest(string p_userCode, string userCode)
    {
        WWWForm form = new WWWForm();
        form.AddField("web_userCode", p_userCode);
        form.AddField("vr_userCode", userCode);

        
            bool isMatchingSuccess = false;
            for (int i = 0; i < 5; i++)
            {
                using (UnityWebRequest www = UnityWebRequest.Post(matchingUrl, form)){
                    yield return www.SendWebRequest();

                    if (www.result != UnityWebRequest.Result.Success)
                    {
                        Debug.LogError("매칭 실패: " + www.error);
                        OpenMessageWindow("매칭 실패: " + www.error, matchingCanvas);
                    }
                    else
                    {
                        string responseText = www.downloadHandler.text;
                        Debug.Log(responseText);

                        if (responseText.Equals("매칭 성공") && !isMatchingSuccess)
                        {
                            Debug.Log("매칭 성공");
                            isMatchingSuccess = true;
                        }
                        yield return new WaitForSeconds(1f);
                    }
                }
            }
            if (isMatchingSuccess)
            {
                CloseLoadingWindow();
                OpenMessageWindow("매칭 성공!", matchingCanvas);
                Debug.Log("씬넘어가유~");
                sceneController.GoToScene(1);
            }
             else
            {
                CloseLoadingWindow();
                OpenMessageWindow("매칭 실패: TimeOut", matchingCanvas);
            }
                
        
        /* else
        {
            Debug.Log("알 수 없는 예외");
            OpenMessageWindow("알 수 없는 예외:RMR2", matchingCanvas);
        } */  
    }
}