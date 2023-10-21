using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public GameObject prefab;
    public TMP_Text camNumberText;

    public Vector3 position1;
    public Vector3 position2;
    public Quaternion rotation1 = Quaternion.Euler(0f, 90f, 270f);
    public Quaternion rotation2 = Quaternion.Euler(0f, 270f, 270f);
    public void Start()
    {
        // MapGenerator 스크립트를 가진 게임 오브젝트를 찾습니다.
        MapGenerator aScript = FindObjectOfType<MapGenerator>();

        if (aScript != null)
        {
            // MapGenerator 스크립트의 camera_p 변수에 접근하여 사용할 수 있습니다.
            RectInt bRect = aScript.camera_p;
            camera_po_width = bRect.width;
            camera_po_height = bRect.height;
            cctv_5_po = new Vector3(camera_po_width, 3, camera_po_height - 2);
            cctv_6_po = new Vector3(camera_po_width, 3, camera_po_height + 2);
            position1 = new Vector3(camera_po_width+0.35f, 3.05f, camera_po_height - 2);
            position2 = new Vector3(camera_po_width+0.35f, 3.05f, camera_po_height + 2);
        }
        // 첫 번째 프리팹 생성
        GameObject instance1 = Instantiate(prefab, position1, rotation1);
        instance1.name = "CCTV5";
        // 두 번째 프리팹 생성
        GameObject instance2 = Instantiate(prefab, position2, rotation2);
        instance2.name = "CCTV6";
    }

    public void MoveCCTV1()
    {
        transform.position = cctv_1_po;
        transform.rotation = Quaternion.Euler(cctv_1_ro);
        camNumberText.text = "CAM 01";
    }
    public void MoveCCTV2()
    {
        transform.position = cctv_2_po;
        transform.rotation = Quaternion.Euler(cctv_2_ro);
        camNumberText.text = "CAM 02";
    }
    public void MoveCCTV3()
    {
        transform.position = cctv_3_po;
        transform.rotation = Quaternion.Euler(cctv_3_ro);
        camNumberText.text = "CAM 03";
    }
    public void MoveCCTV4()
    {
        transform.position = cctv_4_po;
        transform.rotation = Quaternion.Euler(cctv_4_ro);
        camNumberText.text = "CAM 04";
    }
    public void MoveCCTV5()
    {
        transform.position = cctv_5_po;
        transform.rotation = Quaternion.Euler(cctv_5_ro);
        camNumberText.text = "CAM 05";
    }
    public void MoveCCTV6()
    {
        transform.position = cctv_6_po;
        transform.rotation = Quaternion.Euler(cctv_6_ro);
        camNumberText.text = "CAM 06";
    }
}
