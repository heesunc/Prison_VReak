using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class LoginManager : MonoBehaviour
{
    [SerializeField] public TMP_InputField InputField_UserCode;
    [SerializeField] public TMP_InputField InputField_PW;
    public SceneTransitionManager sceneController;
    public TrackedDeviceGraphicRaycaster loginCanvasRaycaster;
    public GameObject msgCanvas;
    public TMP_Text msgText;
    // public Text loginStatusText;

    private string loginUrl = "https://prisonvreak.store/auth/vr_login_process"; // 로그인 처리를 수행하는 서버 스크립트의 URL을 지정해야 합니다.

    public vouserCode OnLoginButtonClicked()
    {
        string userCode = InputField_UserCode.text;
        string pwd = InputField_PW.text;

        StartCoroutine(LoginRequest(userCode, pwd));
    }

    public void OpenMessageWindow(string msg)
    {
        msgCanvas.SetActive(true);
        msgText.SetText(msg);
        loginCanvasRaycaster.enabled = false;
    }

    private IEnumerator LoginRequest(string id, string pwd)
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
                // loginStatusText.text = "로그인 요청 실패";
                OpenMessageWindow("로그인 요청 실패: " + www.error);
            }
            else
            {
                string responseText = www.downloadHandler.text;

                if (responseText.Equals("로그인 정보가 일치하지 않습니다."))
                {
                    Debug.Log("로그인 정보가 일치하지 않습니다.");
                    // loginStatusText.text = "로그인 정보가 일치하지 않습니다.";
                    OpenMessageWindow("로그인 정보가 일치하지 않습니다.");
                }
                else if (responseText.Equals("아이디와 비밀번호를 입력하세요."))
                {
                    Debug.Log("아이디와 비밀번호를 입력하세요.");
                    // loginStatusText.text = "아이디와 비밀번호를 입력하세요.";
                    OpenMessageWindow("아이디와 비밀번호를 입력하세요.");
                }
                else
                {
                    UserInfo userInfo = JsonUtility.FromJson<UserInfo>(responseText);

                    // 로그인 후 받아온 정보를 다음 씬에 전달
                    GameObject userInfoObject = new GameObject("UserInfoObject");
                    DontDestroyOnLoad(userInfoObject);
                    userInfoObject.AddComponent<UserInfoContainer>().SetUserInfo(userInfo);

                    Debug.Log("로그인 성공");

                    // 씬 전환
                    sceneController.GoToScene(1);
                    // loginStatusText.text = "로그인 성공";
                    // 로그인 성공 시 처리
                }
            }
        }
    }
}