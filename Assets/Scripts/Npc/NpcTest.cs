using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcTest : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private int currentDestinationIndex = 0;

    public Transform target; // 목적지
    public Animator anim;
    Rigidbody rigid;
    NavMeshAgent agent;
    //[SerializeField] private Transform[] tf_Destination; // 좌표값
    Vector3[] lightPositionsArray; // lightposition 값으로 교도관 순찰

    // 열거형으로 정해진 상태값 이용
    enum State
    {
        Idle, // Player를 찾는다, 찾았으면 Run상태로 전이하고 싶다.
        Run, // 타겟방향으로 이동(요원)
        Attack // 일정 시간마다 공격
    }
    //상태 처리
    State state;

    // Start is called before the first frame update
    void Start()
    {
        //생성시 상태를 Idle로 한다.
        state = State.Idle;
        target = GameObject.FindGameObjectWithTag("Player").transform;

        rigid = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        // mapgenerator에서 배열로 변환한 빛 위치 정보를 가져옴
        MapGenerator mapGen = FindObjectOfType<MapGenerator>();
        lightPositionsArray = mapGen.GetLightPositionsArray();

        // light는 공중에 있어서 y 값을 변경해서 가져와야 함
        float newYValue = 0.15f;
        for (int i = 0; i < lightPositionsArray.Length; i++)
        {
            lightPositionsArray[i] = new Vector3(lightPositionsArray[i].x, newYValue, lightPositionsArray[i].z);
        }
        lightPositionsArray[lightPositionsArray.Length - 1] = transform.position;

        //lightPositionsArray = new Vector3[tf_Destination.Length + 1]; // originPos를 기억하기 위해 +1

        /*for (int i = 0; i < tf_Destination.Length; i++)
        {
            lightPositionsArray[i] = tf_Destination[i].position;

        }
        lightPositionsArray[lightPositionsArray.Length - 1] = transform.position;*/
    }

    // 포지션 Scene에서 확인하려고 작성한 것.
    private void OnDrawGizmos()
    {
        if (lightPositionsArray != null)
        {
            Gizmos.color = Color.yellow;
            for (int i = 0; i < lightPositionsArray.Length; i++)
            {
                Gizmos.DrawSphere(lightPositionsArray[i], 0.2f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //만약 state가 idle이라면
        if (state == State.Idle)
        {
            UpdateIdle();
        }
        else if (state == State.Run)
        {
            UpdateRun();
        }
        else if (state == State.Attack)
        {
            UpdateAttack();
        }
    }

    void Patrol()
    {
        // 에이전트가 현재 순찰 지점에 도달했는지 확인
        if (Vector3.Distance(transform.position, lightPositionsArray[currentDestinationIndex]) <= 1f)
        {
            // 디버그 문장 추가
            Debug.Log("Reached patrol point " + currentDestinationIndex);

            // 다음 순찰 지점으로 이동
            currentDestinationIndex = (currentDestinationIndex + 1) % lightPositionsArray.Length;
            agent.SetDestination(lightPositionsArray[currentDestinationIndex]);

            // 디버그 문장으로 인덱스 확인
            Debug.Log("New destination index: " + currentDestinationIndex);
        }
        //Debug.Log("NPC Patrol");
    }

    private void UpdateIdle() // 순찰
    {
        agent.speed = 5;

        Patrol();

        // target과의 거리가 10보다 작거나 같으면 Run
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= 10f)
        {
            state = State.Run;
            //이렇게 state값을 바꿨다고 animation까지 바뀔까? no! 동기화를 해줘야한다.
            anim.SetTrigger("Run");
            anim.ResetTrigger("Idle");
        }
        else
        {
            state = State.Idle;
            anim.SetTrigger("Idle");
            //anim.ResetTrigger("Run");

            //currentDestinationIndex = 0;
            agent.SetDestination(lightPositionsArray[currentDestinationIndex]);
        }
        //Debug.Log("NPC UpdateIdle");
    }

    private void UpdateRun()
    {
        // Run하다가 남은 거리가 2미터라면 공격한다.
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= 2f)
        {
            state = State.Attack;
            anim.SetTrigger("Attack");
            anim.ResetTrigger("Run");
        }
        else if (distance > 10f) // 멀어지면 다시 Idle 상태로
        {
            state = State.Idle;
            anim.SetTrigger("Idle");
            anim.ResetTrigger("Run");
        }

        //타겟 방향으로 이동하다가
        agent.speed = 3.5f;
        //요원에게 목적지를 알려준다.
        //if (state == State.Run)
        agent.destination = target.transform.position;

        //Debug.Log("NPC UpdateRun");
    }

    private void UpdateAttack()
    {
        agent.speed = 0;
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance > 2f)
        {
            state = State.Run;
            anim.SetTrigger("Run");
            anim.ResetTrigger("Attack");
        }
        /*else if (distance > 10f)
        {
            state = State.Idle;
            anim.SetTrigger("Idle");
            anim.ResetTrigger("Attack");
        }*/
        else
        {
            anim.SetTrigger("Attack");
            anim.ResetTrigger("Run");
            anim.ResetTrigger("Idle");
        }
        //Debug.Log("NPC UpdateAttack");
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