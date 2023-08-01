using UnityEngine;
using UnityEngine.UI;
using Unity.RenderStreaming.Samples;
using Unity.RenderStreaming;

namespace Unity.RenderStreaming.Samples
{
    class WebBrowserInputSample : MonoBehaviour
    {
        [SerializeField] SignalingManager renderStreaming;
        [SerializeField] Transform[] cameras;

        RenderStreamingSettings settings;

        private void Awake()
        {
            settings = SampleManager.Instance.Settings;
        }

        // Start is called before the first frame update
        void Start()
        {

            if (renderStreaming.runOnAwake)
                return;

           /*  if (settings != null)
                renderStreaming.useDefaultSettings = settings.UseDefaultSettings;
            if (settings?.SignalingSettings != null)
                renderStreaming.SetSignalingSettings(settings.SignalingSettings); */
            renderStreaming.Run();
        }
    }
}
