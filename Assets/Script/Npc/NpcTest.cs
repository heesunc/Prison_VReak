using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcTest : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private int currentDestinationIndex = 0;

    public Transform target; // ������
    public Animator anim;
    Rigidbody rigid;
    NavMeshAgent agent;
    [SerializeField] private Transform[] tf_Destination; // ��ǥ��
    private Vector3[] wayPoints;

    // ���������� ������ ���°� �̿�
    enum State
    {
        Idle, // Player�� ã�´�, ã������ Run���·� �����ϰ� �ʹ�.
        Run, // Ÿ�ٹ������� �̵�(���)
        Attack // ���� �ð����� ����
    }
    //���� ó��
    State state;

    // Start is called before the first frame update
    void Start()
    {
        //������ ���¸� Idle�� �Ѵ�.
        state = State.Idle;
        target = GameObject.FindGameObjectWithTag("Player").transform;

        rigid = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        wayPoints = new Vector3[tf_Destination.Length + 1]; // originPos�� ����ϱ� ���� +1

        for (int i = 0; i < tf_Destination.Length; i++)
        {
            wayPoints[i] = tf_Destination[i].position;
        }
        wayPoints[wayPoints.Length - 1] = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //���� state�� idle�̶��
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
        // wayPoints�� ������ ����
        /*for (int i = 0; i < wayPoints.Length; i++)
        {
            // �Ÿ��� 1���� �۰ų� ������ �ش� ������ ������ ������ �Ǵ�
            if (Vector3.Distance(transform.position, wayPoints[i]) <= 1f)
            {
                // ���� ������ ������ ������ �ƴ� ��� ���� wayPoints�� �̵�
                if (i != wayPoints.Length - 1)
                    agent.SetDestination(wayPoints[i + 1]);
                // ���� ������ ������ ������ ��� ���� �ִ� ������ �̵�
                else
                    agent.SetDestination(wayPoints[0]);
            }
        }*/
        // ������Ʈ�� ���� ���� ������ �����ߴ��� Ȯ��
        if (Vector3.Distance(transform.position, wayPoints[currentDestinationIndex]) <= 1f)
        {
            // ���� ���� �������� �̵�
            currentDestinationIndex = (currentDestinationIndex + 1) % wayPoints.Length;
            agent.SetDestination(wayPoints[currentDestinationIndex]);
        }
        //Debug.Log("NPC Patrol");
    }

    private void UpdateIdle() // ����
    {
        agent.speed = 5;
        Patrol();        

        // target���� �Ÿ��� 10���� �۰ų� ������ Run
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= 10f)
        {
            state = State.Run;
            //�̷��� state���� �ٲ�ٰ� animation���� �ٲ��? no! ����ȭ�� ������Ѵ�.
            anim.SetTrigger("Run");
            anim.ResetTrigger("Idle");
        }
        else
        {
            state = State.Idle;
            anim.SetTrigger("Idle");
            anim.ResetTrigger("Run");

            currentDestinationIndex = 0;
            agent.SetDestination(wayPoints[currentDestinationIndex]);
        }

        Debug.Log("NPC UpdateIdle");
    }

    private void UpdateRun()
    {
        // Run�ϴٰ� ���� �Ÿ��� 2���Ͷ�� �����Ѵ�.
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= 2f)
        {
            state = State.Attack;
            anim.SetTrigger("Attack");
            anim.ResetTrigger("Run");
        }
        else if (distance > 10f) // �־����� �ٽ� Idle ���·�
        {
            state = State.Idle;
            anim.SetTrigger("Idle");
            anim.ResetTrigger("Run");
        }

        //Ÿ�� �������� �̵��ϴٰ�
        agent.speed = 3.5f;
        //������� �������� �˷��ش�.
        //if (state == State.Run)
        agent.destination = target.transform.position;

        Debug.Log("NPC UpdateRun");
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
        /*else
        {
            anim.SetTrigger("Attack");
            anim.ResetTrigger("Run");
            anim.ResetTrigger("Idle");
        }*/
        Debug.Log("NPC UpdateAttack");
    }

    void FreezeVelocity()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
        // ������ (Player�� Enemy�� �ε���)�� NavAgent �̵��� �������� �ʵ���
        // ���ָ� �÷��̾� ���~�ϸ鼭 ������ 
    }

    void FixedUpdate()
    {
        FreezeVelocity();
    }
}

