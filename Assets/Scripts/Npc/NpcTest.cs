using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class NpcTest : MonoBehaviour
{
    [SerializeField] private float idleSpeed;
    [SerializeField] private float runSpeed;
    private int currentDestinationIndex = 0;

    public Transform target; // ������
    public Animator anim;
    Rigidbody rigid;
    NavMeshAgent agent;
    Vector3[] lightPositionsArray; // lightposition ������ ������ ����

    private bool isFrozen = false; // NPC�� ����� �������� ���θ� ��Ÿ���� �÷���
    [Header("게임 시작 전 준비")]
    public GameObject Prison_door; // Unity Inspector에서 감옥 입구를 할당
    int door_speed = 0;
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
        isFrozen = true;
        //������ ���¸� Idle�� �Ѵ�.
        state = State.Idle;
        target = GameObject.FindGameObjectWithTag("Player").transform;

        rigid = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        // mapgenerator���� �迭�� ��ȯ�� �� ��ġ ������ ������
        MapGenerator mapGen = FindObjectOfType<MapGenerator>();
        lightPositionsArray = mapGen.GetLightPositionsArray();

        // light�� ���߿� �־ y ���� �����ؼ� �����;� ��
        float newYValue = 0.15f;
        for (int i = 0; i < lightPositionsArray.Length; i++)
        {
            lightPositionsArray[i] = new Vector3(lightPositionsArray[i].x, newYValue, lightPositionsArray[i].z);
        }
        lightPositionsArray[lightPositionsArray.Length - 1] = transform.position;

        //lightPositionsArray = new Vector3[tf_Destination.Length + 1]; // originPos�� ����ϱ� ���� +1
    }

    // ������ Scene���� Ȯ���Ϸ��� �ۼ��� ��.
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
        if (!isFrozen) // NPC�� ����� ���°� �ƴ� ��쿡�� ������Ʈ
        {
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

        // P Ű�� ������ NPC�� �󸰴�.
        if (Input.GetKeyDown(KeyCode.P))
        {
            FreezeNPCFun();
        }
    }

    public void FreezeNPCFun()
    {
        StartCoroutine(FreezeNPC());
        Debug.Log("�ʻ��!!!!!!!!!!!!!!!!!!!");
    }

    IEnumerator FreezeNPC()
    {
        // �ִϸ��̼��� ���߰� ������Ʈ�� �����.
        anim.enabled = false;
        agent.speed = 0;
        isFrozen = true;

        yield return new WaitForSeconds(5f); // 5�� ���

        // 5�� �Ŀ� �ִϸ��̼��� �ٽ� �����ϰ� ������Ʈ�� ���� ���·� ����
        isFrozen = false;
        anim.enabled = true;
        if (state == State.Idle)
        {
            anim.SetTrigger("Idle");
        }
        else if (state == State.Run)
        {
            anim.SetTrigger("Run");
        }
        else if (state == State.Attack)
        {
            anim.SetTrigger("Attack");
        }
        agent.speed = (state == State.Idle) ? idleSpeed : ((state == State.Run) ? runSpeed : 0f); // ���¿� ���� ������Ʈ �ӵ� ����
    }

    void Patrol()
    {
        // ������Ʈ�� ���� ���� ������ �����ߴ��� Ȯ��
        if (Vector3.Distance(transform.position, lightPositionsArray[currentDestinationIndex]) <= 1f)
        {
            // ����� ���� �߰�
            // Debug.Log("Reached patrol point " + currentDestinationIndex);

            // ���� ���� �������� �̵�
            currentDestinationIndex = (currentDestinationIndex + 1) % lightPositionsArray.Length;
            agent.SetDestination(lightPositionsArray[currentDestinationIndex]);

            // ����� �������� �ε��� Ȯ��
            // Debug.Log("New destination index: " + currentDestinationIndex);
        }
        //Debug.Log("NPC Patrol");
    }

    private void UpdateIdle() // ����
    {
        agent.speed = idleSpeed;

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
            //anim.ResetTrigger("Run");

            //currentDestinationIndex = 0;
            agent.SetDestination(lightPositionsArray[currentDestinationIndex]);
        }
        //Debug.Log("NPC UpdateIdle");
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
        agent.speed = runSpeed;
        //������� �������� �˷��ش�.
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
        // ������ (Player�� Enemy�� �ε���)�� NavAgent �̵��� �������� �ʵ���
        // ���ָ� �÷��̾� ���~�ϸ鼭 ������ 
    }

    void FixedUpdate()
    {
        FreezeVelocity();
    }
    ///////////////////////
    ////// 여기서부터는 웹에서 시작하기 위한 준비과정 ////////
    ////////// 시작할때 NPC와 출구를 닫고 웹에서 눌러주면 문열리고 NPC이동가능 //////////
    public void web_Start()
    {
            StartCoroutine(RotatePrisonDoor());
            isFrozen = false;
            GameManager.Instance.SetStartTime(DateTime.Now);
    }

    private IEnumerator RotatePrisonDoor()
    {

        if (door_speed == 0)
        {
            while (door_speed < 72)
            {
                Prison_door.transform.rotation = Quaternion.Euler(0, door_speed + 180, 0);
                door_speed += 2;

                yield return new WaitForSeconds(0.001f); // 0.1초 대기
            }
        }
    }


    ///////////////////////
}