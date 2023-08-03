using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform target; // 목적지
    NavMeshAgent agent; // 요원(enemy)
    Rigidbody rigid;
    public Animator anim;

    // 열거형으로 정해진 상태값 이용
    enum State
    {
        Idle, // Player를 찾는다, 찾았으면 Run상태로 전이하고 싶다.
        Run, // 타겟방향으로 이동(요원)
        Attack // 일정 시간마다 공격
    }
    //상태 처리
    State state;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        //anim = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        //생성시 상태를 Idle로 한다.
        state = State.Idle;
        //요원을 정의
        agent = GetComponent<NavMeshAgent>();
    }

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

        //생성될때 목적지(Player)를 찿는다.
        //target = GameObject.Find("Player").transform;
        //요원에게 목적지를 알려준다.
        //agent.destination = target.transform.position;
    }

    private void UpdateAttack()
    {
        agent.speed = 0;
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance > 2)
        {
            state = State.Run;
            anim.SetTrigger("Run");
        }
        Debug.Log("때림");
    }

    private void UpdateRun()
    {
        //남은 거리가 2미터라면 공격한다.
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= 2)
        {
            state = State.Attack;
            anim.SetTrigger("Attack");
        }

        //타겟 방향으로 이동하다가
        agent.speed = 3.5f;
        //요원에게 목적지를 알려준다.
        agent.destination = target.transform.position;

        Debug.Log("달림");
    }

    private void UpdateIdle()
    {
        agent.speed = 5;
        //생성될때 목적지(Player)를 찿는다.
        target = GameObject.Find("Player").transform;
        
        //target을 찾으면 Run상태로 전이하고 싶다.
        if (target != null)
        {
            state = State.Run;
            //이렇게 state값을 바꿨다고 animation까지 바뀔까? no! 동기화를 해줘야한다.
            anim.SetTrigger("Run");
        }
        Debug.Log("Idle상태");
    }
}