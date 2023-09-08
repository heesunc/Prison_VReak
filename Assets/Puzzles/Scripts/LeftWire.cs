using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftWire : MonoBehaviour
{
    public EWireColor WireColor { get; private set;}

    public bool IsConnected { get; private set;}

    [SerializeField]
    private List<Image> mWireImages;

    [SerializeField]
    private Image mLightImage;

    [SerializeField]
    private RightWire mConnectedWire;

    [SerializeField]
    private RectTransform mWireBody;


    private Canvas mGameCanvas;

    // Start is called before the first frame update
    void Start()
    {
        mGameCanvas = FindObjectOfType<Canvas>();
    }


    public void SetTarget(Vector3 targetPosition, float offset)                                            //각도, 거리, 회전 등 전선이 플레이어 컨트롤에 따라 움직임
    {
        float angle = Vector2.SignedAngle(transform.position + Vector3.right - transform.position,
                targetPosition - transform.position);
            float distance = Vector2.Distance(mWireBody.transform.position, targetPosition) + offset;
            mWireBody.localRotation = Quaternion.Euler(new Vector3(0f,0f,angle));
            mWireBody.sizeDelta = new Vector2(distance * (1 / mGameCanvas.transform.localScale.x), mWireBody.sizeDelta.y);
    }
    public void ResetTarget()
    {
        mWireBody.localRotation = Quaternion.Euler(Vector3.zero);
        mWireBody.sizeDelta = new Vector2(0f, mWireBody.sizeDelta.y);

    }                                                                             //전선 위치 및 이동 초기화
    public void SetWireColor(EWireColor wireColor)                                //전선 색칠
    {
        WireColor = wireColor;
        Color color = Color.black;
        switch(WireColor)                                    
        {
            case EWireColor.Red:
                color = Color.red;
                break;
            case EWireColor.Blue:
                color = Color.blue;
                break;
            case EWireColor.Yellow:
                color = Color.yellow;
                break;
            case EWireColor.Magenta:
                color = Color.magenta;
                break;
        }
        foreach(var image in mWireImages)
        {
            image.color = color;
        }
    }

    public void ConnectWire(RightWire rightWire)
    {
        if(mConnectedWire != null && mConnectedWire != rightWire)
        {
            mConnectedWire.DisconnectWire(this);
            mConnectedWire = null;
        }

        mConnectedWire = rightWire;
        if(mConnectedWire.WireColor == WireColor)
        {
            mLightImage.color = Color.yellow;
            IsConnected = true;
        }                                                                //연결 성공 시 노란색 불들어 옴
    }

    public void DisconnectWire()
    {
        if(mConnectedWire != null)
        {
            mConnectedWire.DisconnectWire(this);
            mConnectedWire = null;
        }
        mLightImage.color = Color.gray;
        IsConnected = false;                                           //연결이 안되어 있는 경우 회색 불 들어옴
    }
}
