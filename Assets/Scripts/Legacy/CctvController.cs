using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CctvController : MonoBehaviour
{
    //카메라 위치 5, 6번 변수
    int camera_po_width;
    int camera_po_height;
    // cctv 포지션 로테이션 위치
    public Vector3 cctv_1_po;
    public Vector3 cctv_2_po;
    public Vector3 cctv_3_po;
    public Vector3 cctv_4_po;
    public Vector3 cctv_5_po;
    public Vector3 cctv_6_po;
    public Vector3 cctv_1_ro;
    public Vector3 cctv_2_ro;
    public Vector3 cctv_3_ro;
    public Vector3 cctv_4_ro;
    public Vector3 cctv_5_ro;
    public Vector3 cctv_6_ro;
    public void Start()
    {
        // MapGenerator 스크립트를 가진 게임 오브젝트를 찾습니다.
        MapGenerator aScript = FindObjectOfType<MapGenerator>();

        if (aScript != null)
        {
            // MapGenerator 스크립트의 camera5 변수에 접근하여 사용할 수 있습니다.
            RectInt bRect = aScript.camera5;
            camera_po_width = bRect.width;
            camera_po_height = bRect.height;
        }
        cctvtransform();
    }
    //카메라 위치 회전 지정
    public void cctvtransform(){
    cctv_1_po = new Vector3(-0.8f, 2.7f, -0.8f);
    cctv_2_po = new Vector3(-0.8f, 2.7f, 50.5f);
    cctv_3_po = new Vector3(50.5f, 2.7f, 50.5f);
    cctv_4_po = new Vector3(50.5f, 2.7f, -0.8f);
    cctv_5_po = new Vector3(camera_po_width, 2, camera_po_height - 2);
    cctv_6_po = new Vector3(camera_po_width, 2, camera_po_height + 2);
    cctv_1_ro = new Vector3(31, 44, 0);
    cctv_2_ro = new Vector3(31, 135, 0);
    cctv_3_ro = new Vector3(31, -135, 0);
    cctv_4_ro = new Vector3(31, -45, 0);
    cctv_5_ro = new Vector3(31, 0, 0);
    cctv_6_ro = new Vector3(31, 180, 0);
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
    public void MoveCCTV6()
    {
        transform.position = cctv_6_po;
        transform.rotation = Quaternion.Euler(cctv_6_ro);
    }
}
