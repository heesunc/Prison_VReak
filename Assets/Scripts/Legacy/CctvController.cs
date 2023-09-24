using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CctvController : MonoBehaviour
{
    int a_width;
    int a_height;
     [SerializeField] public Vector3 cctv_1_po;
     public Vector3 cctv_2_po;
     public Vector3 cctv_3_po;
     public Vector3 cctv_4_po;
     public  Vector3 cctv_5_po;
     public Vector3 cctv_1_ro;
    public  Vector3 cctv_2_ro;
     public Vector3 cctv_3_ro;
     public Vector3 cctv_4_ro;
     public  Vector3 cctv_5_ro;
public void Start()
    {
        // A 스크립트를 가진 게임 오브젝트를 찾습니다.
        MapGenerator aScript = FindObjectOfType<MapGenerator>();

        if (aScript != null)
        {
            // A 스크립트의 B 변수에 접근하여 사용할 수 있습니다.
            RectInt bRect = aScript.camera5;
            a_width = bRect.width;
            a_height = bRect.height;
            Debug.Log("ahffk" + bRect);
            Debug.Log("받아온 width : " + a_width + "받아온 height" + a_height);
            // bRect를 사용할 수 있습니다.
        }
        abc();
    }

    public void abc(){
    cctv_1_po = new Vector3(-0.8f, 3.8f, -0.9f);
    cctv_2_po = new Vector3(-0.8f, 3.8f, 50.5f);
    cctv_3_po = new Vector3(50.5f, 3.8f, 50.5f);
    cctv_4_po = new Vector3(50.5f, 3.8f, -0.8f);
    cctv_5_po = new Vector3(a_width, 3.8f, a_height);
    cctv_1_ro = new Vector3(31, 44, 0);
    cctv_2_ro = new Vector3(31, 130, 0);
    cctv_3_ro = new Vector3(31, -140, 0);
    cctv_4_ro = new Vector3(31, -40, 0);
    cctv_5_ro = new Vector3(31, 0, 0);
    }
    public void MoveCCTV1()
    {
        transform.position = cctv_1_po;
        transform.rotation = Quaternion.Euler(cctv_1_ro);
    }
    public void MoveCCTV2()
    {
        transform.position = cctv_2_po;
        transform.rotation = Quaternion.Euler(cctv_2_ro);
    }
    public void MoveCCTV3()
    {
        transform.position = cctv_3_po;
        transform.rotation = Quaternion.Euler(cctv_3_ro);
    }
    public void MoveCCTV4()
    {
        transform.position = cctv_4_po;
        transform.rotation = Quaternion.Euler(cctv_4_ro);
    }
    public void MoveCCTV5()
    {
        transform.position = cctv_5_po;
        transform.rotation = Quaternion.Euler(cctv_5_ro);
    }
}
