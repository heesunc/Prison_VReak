using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcTest : MonoBehaviour
{
    Rigidbody rigid;
    [SerializeField] private float moveSpeed;
    Enemy enemy;

    NavMeshAgent agent;
    [SerializeField] private Transform[] tf_Destination; // 좌표값
    private Vector3[] wayPoints;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Enemy>();
        wayPoints = new Vector3[tf_Destination.Length + 1]; // originPos를 기억하기 위해 +1

        for (int i = 0; i < tf_Destination.Length; i++)
        {
            wayPoints[i] = tf_Destination[i].position;
        }
        wayPoints[wayPoints.Length - 1] = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    private void Patrol() // 순찰
    {
        // wayPoints의 지점을 순찰
        for (int i = 0; i < wayPoints.Length; i++)
        {
            // 거리가 1보다 작거나 같으면 해당 지점에 도착한 것으로 판단
            if (Vector3.Distance(transform.position, wayPoints[i]) <= 1f)
            {
                // 도착 지점이 마지막 지점이 아닐 경우 다음 wayPoints로 이동
                if (i != wayPoints.Length - 1)
                    agent.SetDestination(wayPoints[i + 1]);
                // 도착 지점이 마지막 지점일 경우 원래 있던 곳으로 이동
                else
                    agent.SetDestination(wayPoints[0]);
            }
        }
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemy.gameObject.SetActive(true);
            Debug.Log("플레이어와 NPC 충돌 후 enemy Script 활성화");
        }
    }

    void FreezeVelocity()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
        // 물리력 (Player와 Enemy의 부딪힘)이 NavAgent 이동을 방해하지 않도록
        // 없애면 플레이어 띠용~하면서 점프됨 
    }

    void FixedUpdate()
    {
        FreezeVelocity();
    }
}

