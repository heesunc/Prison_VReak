using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CctvController : MonoBehaviour
{

    public Vector3 cctv_1_po = new Vector3(2, 15, 1);
    public Vector3 cctv_2_po = new Vector3(1, 15, 99);
    public Vector3 cctv_3_po = new Vector3(99, 15, 1);
    public Vector3 cctv_4_po = new Vector3(98, 15, 98);
    public Vector3 cctv_5_po = new Vector3(2, 15, 1);
    public Vector3 cctv_1_ro = new Vector3(39, 40, 0);
    public Vector3 cctv_2_ro = new Vector3(39, -266, 0);
    public Vector3 cctv_3_ro = new Vector3(39, -45, 0);
    public Vector3 cctv_4_ro = new Vector3(39, -137, 0);
    public Vector3 cctv_5_ro = new Vector3(39, 40, 0);

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
