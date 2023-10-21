using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit;

public class NpcTest : MonoBehaviour
{
    AudioSource audioSource;
    Rigidbody rigid;
    NavMeshAgent agent;
    NpcSound npcSound;

    // SerializeField 속성을 사용하여 private 변수를 Inspector에서 노출시킵니다.
    [SerializeField] private float idleSpeed;
    [SerializeField] private float runSpeed;
    private int currentDestinationIndex = 0;

    public Transform target; // 타겟(Transform 객체)을 저장할 변수
    public Animator anim;

    Vector3[] lightPositionsArray; // 라이트의 위치를 저장할 배열

    public bool isFrozen = false; // NPC가 움직이는지 여부를 나타내는 변수

    [Header("상태에 따른 오디오")]
    //public AudioClip audioIdle;
    public AudioClip audioRun;
    public AudioClip audioAttack;
    public AudioClip audioFreeze;

    [Header("게임 시작 전 준비")]
    public GameObject prisonDoor; // 감옥 문을 저장할 변수
    public GameObject prisonDoorOutline;
    int door_speed = 0;
    public GameObject particlePrefab; // 파티클 프리팹을 저장할 변수
    public float yOffset = 1.0f; // Y 축으로 파티클을 올릴 거리

    
    bool isWebUnlockDoor = false;
    bool isGameOver = false;
    public GameObject gameOverCanvas;
    public GameObject webGameOverCanvas;
    public GameObject playerXRObject;
    public GameObject CCTVObject;

    enum State
    {
        Idle, // NPC가 대기 상태일 때
        Run, // NPC가 이동 상태일 때
        Attack // NPC가 공격 상태일 때
    }
    State state; // NPC의 상태를 저장하는 열거형 변수

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start 함수는 게임 오브젝트가 활성화될 때 한 번 호출됩니다.
    void Start()
    {
        npcSound = FindObjectOfType<NpcSound>();

        isFrozen = true;
        state = State.Idle; // 게임 시작 시 NPC 상태를 Idle로 초기화
        target = GameObject.FindGameObjectWithTag("Player").transform; // "Player" 태그를 가진 게임 오브젝트를 타겟으로 설정

        rigid = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        // MapGenerator를 찾아서 라이트 위치 배열을 가져옵니다.
        MapGenerator mapGen = FindObjectOfType<MapGenerator>();
        lightPositionsArray = mapGen.GetLightPositionsArray();

        // 라이트 위치 배열의 Y값을 조정합니다.
        float newYValue = 0.15f;
        for (int i = 0; i < lightPositionsArray.Length; i++)
        {
            lightPositionsArray[i] = new Vector3(lightPositionsArray[i].x, newYValue, lightPositionsArray[i].z);
        }
        lightPositionsArray[lightPositionsArray.Length - 1] = transform.position;

        if (GameManager.Instance.isEmergency)
        {
            web_Start();
        }
    }

    // OnDrawGizmos 함수는 에디터 상에서 게임 오브젝트를 선택하면 호출되어 해당 게임 오브젝트의 가시화를 처리합니다.
    private void OnDrawGizmos()
    {
        // 라이트 위치 배열이 존재하는 경우 라이트 위치를 노란색 구로 그립니다.
        if (lightPositionsArray != null)
        {
            Gizmos.color = Color.yellow;
            for (int i = 0; i < lightPositionsArray.Length; i++)
            {
                Gizmos.DrawSphere(lightPositionsArray[i], 0.2f);
            }
        }
    }

    // Update 함수는 매 프레임마다 호출됩니다.
    void Update()
    {
        if (!isFrozen && !isGameOver) // NPC가 움직이지 않는 상태가 아니라면 and 게임 오버 상태가 아니라면
        {
            if (state == State.Idle)
            {
                UpdateIdle();
                //PlayAudio(audioIdle, 1.0f);
                npcSound.PlaySound(0.5f);
            }
            else if (state == State.Run)
            {
                UpdateRun();
                PlayAudio(audioRun, 1.0f);
                npcSound.PlaySound(1.0f);
            }
            // 게임 오버 처리
            else if (state == State.Attack)
            {
                UpdateAttack();
                PlayAudio(audioAttack, 1.0f);
                GameOverProc(gameOverCanvas, webGameOverCanvas);

            }
        }

        if (Input.GetKeyDown(KeyCode.P)) // P 키를 누르면
        {
            FreezeNPCFun();
        }
    }

    public void GameOverProc(GameObject vrCanvas, GameObject webCanvas)
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
        isGameOver = true;
        vrCanvas.SetActive(true);
        webCanvas.SetActive(true);
        playerXRObject.GetComponent<ContinuousMoveProviderBase>().enabled = false;
        CCTVObject.GetComponent<CctvController>().enabled = false;
    }

    // 상태에 따라 오디오 재생 함수
    private void PlayAudio(AudioClip clip, float pitch)
    {
        if (audioSource != null && clip != null)
        {
            if (!audioSource.isPlaying || audioSource.clip != clip)
            {
                audioSource.clip = clip;
                audioSource.pitch = pitch;
                //audioSource.PlayOneShot(clip); // 음악 겹칠 수 있도록
                audioSource.Play();
            }
        }
    }

    public void AttackAnim()
    {
        StartCoroutine(PlayRandomAttackAnimation());
    }

    // Attack 상태에서 무작위 애니메이션 클립 재생
    IEnumerator PlayRandomAttackAnimation()
    {
        // Only set the AttackIndex once
        int randomAttackIndex = UnityEngine.Random.Range(0, 4);
        anim.SetInteger("AttackIndex", randomAttackIndex);

        // Exit the coroutine
        yield break;
    }


    // FreezeNPCFun 함수는 NPC를 일시 정지시키는 코루틴을 실행합니다.
    public void FreezeNPCFun()
    {
        if (!isGameOver)
        {
            // 파티클을 생성할 위치 설정
            Vector3 spawnPosition = transform.position + new Vector3(0, yOffset, 0);
            // 파티클을 생성
            Instantiate(particlePrefab, spawnPosition, Quaternion.identity);
            StartCoroutine(FreezeNPC());
            Debug.Log("NPC를 일시 정지합니다!!!!!!!!!!!!!!!!!!!");
        }
    }

    // FreezeNPC 함수는 NPC를 일시 정지시키는 코루틴입니다.
    IEnumerator FreezeNPC()
    {
        // 애니메이션을 비활성화하고 속도를 0으로 설정하여 NPC를 정지시킵니다.
        anim.enabled = false;
        agent.speed = 0;
        PlayAudio(audioFreeze, 1.0f);
        isFrozen = true;

        yield return new WaitForSeconds(5f); // 5초 동안 대기합니다.

        particle_destroy(); // 파티클을 제거합니다.

        // 5초 후에 NPC를 다시 움직일 수 있도록 상태와 속도를 복원합니다.
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
        agent.speed = (state == State.Idle) ? idleSpeed : ((state == State.Run) ? runSpeed : 0f);
    }

    // Patrol 함수는 NPC의 순찰 동작을 처리합니다.
    void Patrol()
    {
        if (Vector3.Distance(transform.position, lightPositionsArray[currentDestinationIndex]) <= 1f)
        {
            currentDestinationIndex = (currentDestinationIndex + 1) % lightPositionsArray.Length;
            agent.SetDestination(lightPositionsArray[currentDestinationIndex]);
        }
    }

    private void UpdateIdle()
    {
        agent.speed = idleSpeed;
        Patrol();

        // 타겟과의 거리를 계산하여 상태를 변경합니다.
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= 10f)
        {
            state = State.Run;
            anim.SetTrigger("Run");
            anim.ResetTrigger("Idle");
        }
        else
        {
            state = State.Idle;
            anim.SetTrigger("Idle");
            agent.SetDestination(lightPositionsArray[currentDestinationIndex]);
        }
    }

    private void UpdateRun()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= 2f)
        {
            state = State.Attack;
            anim.SetTrigger("Attack");
            AttackAnim();
            anim.ResetTrigger("Run");
        }
        else if (distance > 10f)
        {
            state = State.Idle;
            anim.SetTrigger("Idle");
            anim.ResetTrigger("Run");
        }

        agent.speed = runSpeed;
        agent.destination = target.transform.position;
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
        else
        {
            anim.SetTrigger("Attack");
            anim.ResetTrigger("Run");
            anim.ResetTrigger("Idle");
        }
    }

    void FreezeVelocity()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }

    void FixedUpdate()
    {
        FreezeVelocity();
    }

    // 웹에서 게임 시작을 위한 함수
    public void web_Start()
    {
        isFrozen = false;
        isWebUnlockDoor = true;
        prisonDoorOutline.SetActive(true);
        prisonDoor.GetComponent<XRSimpleInteractable>().enabled = true;
        GameManager.Instance.SetStartTime(DateTime.Now);
    }

    public void DoorOpen(GameObject doorObj)
    {
        if (isWebUnlockDoor) 
        {
            doorObj.GetComponent<AudioSource>().Play();
            StartCoroutine(RotatePrisonDoor(doorObj));
            doorObj.GetComponent<XRSimpleInteractable>().enabled = false;
        }
    }

    // 감옥 문을 회전시키는 코루틴
    private IEnumerator RotatePrisonDoor(GameObject doorObj)
    {
        if (door_speed == 0)
        {
            while (door_speed < 72)
            {
                doorObj.transform.rotation = Quaternion.Euler(0, door_speed + 180, 0);
                door_speed += 2;
                yield return new WaitForSeconds(0.001f);
            }
        }
    }

    // 파티클을 제거하는 함수
    public void particle_destroy()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.name.Contains("Clone"))
            {
                Destroy(obj);
            }
        }
    }
}
