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
    [SerializeField] private Transform[] tf_Destination; // ��ǥ��
    private Vector3[] wayPoints;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Enemy>();
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
        Patrol();
    }

    private void Patrol() // ����
    {
        // wayPoints�� ������ ����
        for (int i = 0; i < wayPoints.Length; i++)
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
        }
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemy.gameObject.SetActive(true);
            Debug.Log("�÷��̾�� NPC �浹 �� enemy Script Ȱ��ȭ");
        }
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

