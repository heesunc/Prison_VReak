using UnityEngine;
using UnityEngine.UI;
using Unity.RenderStreaming.Samples;
using Unity.RenderStreaming;
using System.Threading;


namespace Unity.RenderStreaming.Samples
{
    class WebBrowserInputSample : MonoBehaviour
    {
        [SerializeField] SignalingManager renderStreaming;
        [SerializeField] private AudioSource receiveAudioSource;
        [SerializeField] private AudioStreamReceiver audioStreamReceiver;
        [SerializeField] private SingleConnection singleConnection;
        UserInfoContainer userInfoContainer;
        private string connectionId;
        RenderStreamingSettings settings;

        private void Awake()
        {
            audioStreamReceiver.targetAudioSource = receiveAudioSource;
            audioStreamReceiver.OnUpdateReceiveAudioSource += source =>
            {
                source.Play();
            };
            userInfoContainer = FindObjectOfType<UserInfoContainer>();
            if (userInfoContainer != null)
            {
                UserInfo loadedUserInfo = userInfoContainer.GetUserInfo();

                // 필요한 값 사용
                string vr_userCode = loadedUserInfo.vr_userCode;

                connectionId = vr_userCode;
                Debug.Log(connectionId);
            }
            
        }

        // Start is called before the first frame update
        private void Start()
        {
            
            settings = SampleManager.Instance.Settings;

            if (renderStreaming.runOnAwake)
                return;

            if (settings != null)
                renderStreaming.useDefaultSettings = settings.UseDefaultSettings;
            if (settings?.SignalingSettings != null)
                renderStreaming.SetSignalingSettings(settings.SignalingSettings); 
            renderStreaming.Run();
            
            Thread.Sleep(1000);
            

            singleConnection.CreateConnection(connectionId);
        }
    }
}
