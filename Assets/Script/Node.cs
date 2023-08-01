using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Node leftNode;
    public Node rightNode;
    public Node parNode;
    public RectInt nodeRect; //분리된 공간의 rect정보
    public RectInt roomRect; //분리된 공간 속 방의 rect정보
    public Vector3Int center
    {
        get {
            return new Vector3Int(roomRect.x+roomRect.width/2, 0, roomRect.y + roomRect.height/ 2);
        }
        //방의 가운데 점. 방과 방을 이을 때 사용
    }
    public Node(RectInt rect)
    {
        this.nodeRect = rect;
    }
}



//프로토타입까지
//    입구,출구 만들기
//    퍼즐 하나 (간단하게 스위치 내리기 정도)
//    퍼즐 클리어해서 출구 나가면 게임 클리어 씬
//    적-플레이어 거리 오 이거 누구꺼여?? 오 오....