using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EWireColor                              //전선 색상 배열
{
    None = -1,
    Red,
    Blue,
    Yellow,
    Magenta
}                                                                              

public class FixWiringTask : MonoBehaviour
{
    [SerializeField]
    private List<LeftWire> mLeftWires;

    [SerializeField]
    private List<RightWire> mRightWires;

    private LeftWire mSelectedWire;
 

    private void OnEnable()                                                 //전선에 색깔 입히기
    {
        List<int> numberPool = new List<int>();                            //왼쪽 전선에 랜덤한 색깔 지정
        for(int i = 0; i<4; i++)
        {
            numberPool.Add(i);
        }
        int index = 0;

        while(numberPool.Count != 0)
        {
            var number = numberPool[Random.Range(0,numberPool.Count)];
            mLeftWires[index++].SetWireColor((EWireColor)number);
            numberPool.Remove(number);
        }                                                                


        for(int i = 0; i<4; i++)                                           //오른쪽 전선은 랜덤 배열 말고 위에 정의한 리스트 순서대로
        {
            mRightWires[i].SetWireColor((EWireColor)i);
        }                                                             
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))                                   //클릭 시 전선 선택해주는 기능
        {
            RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.right,1f);
            if(hit.collider != null)
            {
                var left = hit.collider.GetComponentInParent<LeftWire>();
                if(left != null)
                {
                    mSelectedWire = left;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))                                        //클릭 뗐을 때 동작
        {
            if(mSelectedWire != null)
            {
                RaycastHit2D[] hits = Physics2D.RaycastAll(Input.mousePosition, Vector2.right, 1f);
                foreach(var hit in hits)
                {
                    if(hit.collider != null)
                    {
                        var right = hit.collider.GetComponentInParent<RightWire>();
                        if(right != null)
                        {
                            mSelectedWire.SetTarget(hit.transform.position,-50f);
                            mSelectedWire.ConnectWire(right);
                            right.ConnectWire(mSelectedWire);
                            mSelectedWire = null;
                            return;
                        }                                                       //전선이 이어지게 만들어주는 동작
                    }
                }
                mSelectedWire.ResetTarget();                                   //전선 초기화
                mSelectedWire.DisconnectWire();                                //연결 끊기
                mSelectedWire = null;                                          //선택 전선 해제
            }
        }
        if(mSelectedWire != null)
        {
            mSelectedWire.SetTarget(Input.mousePosition,-15f);                //마우스 위치 따라서 전선 이동할 때의 속도 지정
        }
        CheckCompletion();
    }
    // 추가 작성사항 //
     private void CheckCompletion()
    {
        foreach (var leftWire in mLeftWires)
        {
            if (!leftWire.IsConnected)
                return; // 연결되지 않은 전선이 있으면 완료하지 않음
        }

        // 모든 전선이 연결되어 노란색이면 Scene 전환 및 Door 이동 시작
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1f); // 전환 전 잠시 대기
        // Scene 전환
        SceneManager.LoadScene("ClearScene");
    }
    // 추가 작성사항 //
}
