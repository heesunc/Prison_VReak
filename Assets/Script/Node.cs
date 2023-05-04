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
