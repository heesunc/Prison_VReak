using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RightWire : MonoBehaviour
{
    public EWireColor WireColor { get; private set;}

    public bool IsConnected { get; private set; }

    [SerializeField]
    private List<Image> mWireImages;

    [SerializeField]
    private Image mLightImage;

    [SerializeField]
    private List<LeftWire> mConnectedWires = new List<LeftWire>();



    public void SetWireColor(EWireColor wireColor)                                   //전선 색칠
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

    public void ConnectWire(LeftWire leftWire)
    {
        if(mConnectedWires.Contains(leftWire))
        {
            return;                                           //이미 연결 된 전선이면 값을 받지 않음
        }
        mConnectedWires.Add(leftWire);
        if(mConnectedWires.Count == 1 && leftWire.WireColor == WireColor)
        {
            mLightImage.color = Color.yellow;
            IsConnected = true;
        }                                                     //첫 번째로 연결되는 전선이고 색상이 같으면 노란색 불
        else
        {
            mLightImage.color = Color.gray;
            IsConnected = false;                              //아니면 회색
        }
    }
    public void DisconnectWire(LeftWire leftWire)
    {
        mConnectedWires.Remove(leftWire);                     //연결 해제

        if(mConnectedWires.Count == 1 && mConnectedWires[0].WireColor == WireColor)
        {
            mLightImage.color = Color.yellow;
            IsConnected = true;                                      //하나의 전선만 연결되어 있고 그 색상이 일치하면 - 노란색
        }
        else
        {
            mLightImage.color = Color.gray;
            IsConnected = false;                                         //아니면 회색
        }
    }

}