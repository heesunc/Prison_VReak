using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Unity.AI.Navigation;

public class MapGenerator : MonoBehaviour
{
    [Header("맵 노드 생성 관련")]
    [SerializeField] Vector3Int mapSize; //원하는 맵의 크기
    [SerializeField] float minimumDivideRate; //공간이 나눠지는 최소 비율
    [SerializeField] float maximumDivideRate; //공간이 나눠지는 최대 비율
    [SerializeField] private int maximumDepth; //트리의 높이, 높을 수록 방을 더 자세히 나누게 됨
    [Header("프리팹")]
    [SerializeField] private GameObject wallPrefab; // 노드구조물 프리팹
    [SerializeField] private GameObject barPrefab; // 장애물 프리팹
    [SerializeField] private GameObject lightPrefab; // 형광등
    [SerializeField] private GameObject NPCPrefab; // NPC
    [Header("퍼즐 버튼 위치 난수")]
    [SerializeField] private int num = 1;
    [SerializeField] public int a;
    [Header("구조물 리스트")]
    [SerializeField] private List<GameObject> wallList = new List<GameObject>();
    [Header("rect 좌표 리스트")]
    [SerializeField] private List<RectInt> rectList = new List<RectInt>();
    [Header("구조물 4면 중앙값 리스트 - 좌상우하 순서")]
    [SerializeField] private List<Rect> rectCheckList = new List<Rect>();
    [Header("장애물 동적 생성을 위한 리스트")]
    [SerializeField] private List<Rect> leftBlockList = new List<Rect>();
    [SerializeField] private List<Rect> topBlockList = new List<Rect>();
    [SerializeField] private List<Rect> rightBlockList = new List<Rect>();
    [SerializeField] private List<Rect> bottomBlockList = new List<Rect>();
    [Header("퍼즐 버튼 생성을 위한 리스트")]
    [SerializeField] private List<RectInt> nodeList = new List<RectInt>();//나눌때 위치정보
    List<Vector3> collidedSurfacePositions = new List<Vector3>();
    List<Vector3> lightPositions = new List<Vector3>();

  
    // 리스트를 배열로 변환하여 반환하는 메서드
    public Vector3[] GetLightPositionsArray()
    {
        return lightPositions.ToArray();
    }
    public RectInt camera_p;
    private int wallPrefabCounter = 1; // prefab에 번호를 붙이기 위해 만듬
    private int lightPrefabCounter = 1; // prefab에 번호를 붙이기 위해 만듬

    public NavMeshSurface surface;

    ////////// Start //////////
    void Start()
    {

        // 맵 생성
        Node root = new Node(new RectInt(0, 0, mapSize.x, mapSize.y)); //전체 맵 크기의 루트노드를 만듬
        Divide(root, 0, nodeList);
        GenerateRoom(root, 0);
        // 라이트 생성
        CalculateRect();

        // 장애물 동적 생성
        //CreateRandomBlocks();

        // npc 관련 navMesh
        surface.BuildNavMesh();

        CreateCameraPosition();


    }
    ////////// 맵 생성 관련 함수 //////////
    void Divide(Node tree, int n, List<RectInt> nodeList)
    {
        if (n == maximumDepth) return; //내가 원하는 높이에 도달하면 더 나눠주지 않는다.
                                       //그 외의 경우에는

        int maxLength = Mathf.Max(tree.nodeRect.width, tree.nodeRect.height);
        //가로와 세로중 더 긴것을 구한후, 가로가 길다면 위 좌, 우로 세로가 더 길다면 위, 아래로 나눠주게 될 것이다.
        int split = Mathf.RoundToInt(Random.Range(maxLength * minimumDivideRate, maxLength * maximumDivideRate));
        //나올 수 있는 최대 길이와 최소 길이중에서 랜덤으로 한 값을 선택
        if (tree.nodeRect.width >= tree.nodeRect.height) //가로가 더 길었던 경우에는 좌 우로 나누게 될 것이며, 이 경우에는 세로 길이는 변하지 않는다.
        {
            tree.leftNode = new Node(new RectInt(tree.nodeRect.x, tree.nodeRect.y, split, tree.nodeRect.height));
            //왼쪽 노드에 대한 정보다
            //위치는 좌측 하단 기준이므로 변하지 않으며, 가로 길이는 위에서 구한 랜덤값을 넣어준다.
            tree.rightNode = new Node(new RectInt(tree.nodeRect.x + split, tree.nodeRect.y, tree.nodeRect.width - split, tree.nodeRect.height));
            //우측 노드에 대한 정보다
            //위치는 좌측 하단에서 오른쪽으로 가로 길이만큼 이동한 위치이며, 가로 길이는 기존 가로길이에서 새로 구한 가로값을 뺀 나머지 부분이 된다.
        }
        else
        {
            tree.leftNode = new Node(new RectInt(tree.nodeRect.x, tree.nodeRect.y, tree.nodeRect.width, split));
            tree.rightNode = new Node(new RectInt(tree.nodeRect.x, tree.nodeRect.y + split, tree.nodeRect.width, tree.nodeRect.height - split));
            //세로가 더 길었던 경우이다. 자세한 사항은 가로와 같다.
        }
        tree.leftNode.parNode = tree; //자식노드들의 부모노드를 나누기전 노드로 설정
        tree.rightNode.parNode = tree;
        SaveNodeInfo(tree.leftNode, nodeList);
        Divide(tree.leftNode, n + 1, nodeList); // 왼쪽 자식 노드를 먼저 나눈다.
        Divide(tree.rightNode, n + 1, nodeList);

    }
    private RectInt GenerateRoom(Node tree, int n)
    {
        RectInt rect;
        if (n == maximumDepth) //해당 노드가 리프노드라면 방을 만들어 줄 것이다.
        {
            rect = tree.nodeRect;
            int width = Mathf.Max(rect.width - 2, 2);
            //방의 가로 최소 크기는 노드의 가로길이의 절반, 최대 크기는 가로길이보다 1 작게 설정한 후 그 사이 값중 랜덤한 값을 구해준다.
            int height = Mathf.Max(rect.height - 2, 2);
            //높이도 위와 같다.

            rectList.Add(rect);

            int x = rect.x + Random.Range(1, rect.width - width);
            //방의 x좌표이다. 만약 0이 된다면 붙어 있는 방과 합쳐지기 때문에,최솟값은 1로 해주고, 최댓값은 기존 노드의 가로에서 방의 가로길이를 빼 준 값이다.
            int y = rect.y + Random.Range(1, rect.height - height);
            //y좌표도 위와 같다.
            rect = new RectInt(x, y, width, height);

            CreateWalls(rect);
        }
        else
        {
            tree.leftNode.roomRect = GenerateRoom(tree.leftNode, n + 1);
            tree.rightNode.roomRect = GenerateRoom(tree.rightNode, n + 1);
            rect = tree.leftNode.roomRect;
        }
        return rect;
    }
    private void CreateWalls(RectInt rect)
    {
        // wallPrefab을 사용하여 벽을 만듭니다.
        GameObject Wall = Instantiate(wallPrefab);
        Wall.transform.localScale = new Vector3(rect.width - 1, 5, rect.height - 1);
        Wall.transform.position = new Vector3(rect.x + rect.width / 2f, 2.5f, rect.y + rect.height / 2f);
        string wallName = "wallPrefab" + wallPrefabCounter;
        Wall.name = wallName;

        // 생성되는 wallPrefab(n) 의 4개 면의 중앙값 구하기
        // position이 소수점으로 나오는 경우도 있어서 RectInt가 아니라 그냥 Rect 사용함
        Rect wallRect = new Rect(Wall.transform.position.x,
                                        Wall.transform.position.z,
                                        Wall.transform.localScale.x,
                                        Wall.transform.localScale.z);

        for (int i = 0; i < 4; i++)
        {
            Vector3 midPoint = Vector3.zero;

            switch (i)
            {
                case 0: // Left edge
                    midPoint = new Vector3(wallRect.x - wallRect.width / 2f, 0, wallRect.y);
                    break;
                case 1: // Top edge
                    midPoint = new Vector3(wallRect.x, 0, wallRect.y + wallRect.height / 2f);
                    break;
                case 2: // Right edge
                    midPoint = new Vector3(wallRect.x + wallRect.width / 2f, 0, wallRect.y);
                    break;
                case 3: // Bottom edge
                    midPoint = new Vector3(wallRect.x, 0, wallRect.y - wallRect.height / 2f);
                    break;
            }
            rectCheckList.Add(new Rect(midPoint.x, midPoint.z, 1, 1)); // width와 height는 필요없기에 1, 1로 설정
        }

        num++;
        wallPrefabCounter++;
        wallList.Add(Wall);
    }
    //////////////////////////////

    ////////// 각 노드의 모서리 구하기 //////////
    private void CalculateRect()
    {
        foreach (RectInt rect in rectList)
        {
            for (int listNum = 1; listNum <= 4; listNum++)
            {
                Vector3 position = Vector3.zero;

                switch (listNum)
                {
                    case 1: // (- , -)
                        Rect currentRect = new Rect((float)rect.x, (float)rect.y, (float)rect.width, (float)rect.height);
                        // 좌표 구하기
                        currentRect = new Rect(currentRect.x, currentRect.y, currentRect.width, currentRect.height);
                        // 라이트 오브젝트 위치 선정
                        position = new Vector3(currentRect.x, 3.13f, currentRect.y);

                        break;

                    case 2: // (- , +)
                        Rect currentRect2 = new Rect((float)rect.x, (float)rect.y, (float)rect.width, (float)rect.height);

                        currentRect2 = new Rect(currentRect2.x, currentRect2.y + currentRect2.height, currentRect2.width, currentRect2.height);
                        position = new Vector3(currentRect2.x, 3.13f, currentRect2.y);

                        break;

                    case 3: // (+ , -)
                        Rect currentRect3 = new Rect((float)rect.x, (float)rect.y, (float)rect.width, (float)rect.height);

                        currentRect3 = new Rect(currentRect3.x + currentRect3.width, currentRect3.y, currentRect3.width, currentRect3.height);
                        position = new Vector3(currentRect3.x, 3.13f, currentRect3.y);

                        break;

                    case 4: // (+ , +)
                        Rect currentRect4 = new Rect((float)rect.x, (float)rect.y, (float)rect.width, (float)rect.height);

                        currentRect4 = new Rect(currentRect4.x + currentRect4.width, currentRect4.y + currentRect4.height, currentRect4.width, currentRect4.height);
                        position = new Vector3(currentRect4.x, 3.13f, currentRect4.y);

                        break;
                }

                // lightPositions 내에 해당 position이 없으면 추가 후 라이트 생성!
                if (!lightPositions.Contains(position))
                {
                    lightPositions.Add(position);

                    GameObject Light = Instantiate(lightPrefab);
                    Light.transform.position = position;

                    string lightName = "light" + lightPrefabCounter;
                    Light.name = lightName;
                    lightPrefabCounter++;
                }
            }
        }
    }
    //////////////////////////////

    ////////// 노드 정보 출력 //////////
    private void SaveNodeInfo(Node node, List<RectInt> nodeList)
    {
        // 여기에서 node 변수를 사용하여 원하는 정보를 추출하거나 저장할 수 있습니다.
        // 예를 들어, 노드의 위치와 크기를 출력하는 방법은 다음과 같습니다.
        // 노드의 사이즈로 위치를 잡아서 리스트에 담아
        nodeList.Add(node.nodeRect);
    }

    //////////////////////////////

    ////////// 장애물 위치 고를때 사용 //////////
    private void selectNum()
    {
        System.Random random = new System.Random();
        a = random.Next(1, 16);
    }
    //////////////////////////////

    /*
    ////////// 장애물 동적 생성 //////////
    private void CreateRandomBlocks()
    {
        // 변수 초기화
        Vector3 checkPosition = Vector3.zero;

        // rectCheckList에 있는 값들로 foreach 돌림
        foreach (Rect rectPosition in rectCheckList)
        {
            checkPosition = new Vector3(rectPosition.x, 1, rectPosition.y);

            if (checkPosition.x == 1.5f)
            { // 좌측 테두리 후보 리스트에 추가
                leftBlockList.Add(new Rect(checkPosition.x, checkPosition.z, 1, 1));
            }
            else if (checkPosition.z == 48.5f)
            { // 상단 테두리 후보 리스트에 추가
                topBlockList.Add(new Rect(checkPosition.x, checkPosition.z, 1, 1));
            }
            else if (checkPosition.x == 48.5f)
            { // 우측 테두리 후보 리스트에 추가
                rightBlockList.Add(new Rect(checkPosition.x, checkPosition.z, 1, 1));
            }
            else if (checkPosition.z == 1.5f)
            { // 하단 테두리 후보 리스트에 추가
                bottomBlockList.Add(new Rect(checkPosition.x, checkPosition.z, 1, 1));
            }
        }

        // 좌측 테두리 장애물 생성
        Rect leftRect = leftBlockList[Random.Range(1, leftBlockList.Count - 1)]; // 후보 중 랜덤한 값을 고름 // 1부터 시작하는 이유 -> 1번노드 좌측에 생성될때 감옥 입구를 막는 것 방지
        Vector3 leftPosition = new Vector3(leftRect.x - 1, 2.5f, leftRect.y); // 값들에 -1 혹은 +1을 한 이유 -> 그냥 생성하니깐 복도 끝에 안붙어서 해줬어요... 왜 안되는지는 몰?루

        // 상단 테두리 장애물 생성
        Rect topRect = topBlockList[Random.Range(1, topBlockList.Count - 1)];
        Vector3 topPosition = new Vector3(topRect.x, 2.5f, topRect.y + 1);

        // 우측 테두리 장애물 생성
        Rect rightRect = rightBlockList[Random.Range(1, rightBlockList.Count - 1)];
        Vector3 rightPosition = new Vector3(rightRect.x + 1, 2.5f, rightRect.y);

        // 하단 테두리 장애물 생성
        Rect bottomRect = bottomBlockList[Random.Range(1, bottomBlockList.Count - 1)]; // Count - 1 한 이유 -> 16번노드 우측에 생성될때 출구를 막는 것 방지
        Vector3 bottomPosition = new Vector3(bottomRect.x, 2.5f, bottomRect.y - 1);

        // 1~16 중 랜덤 수를 뽑아서 짝수면 "좌우", 홀수면 "상하" 장애물 생성
        selectNum();
        if (a % 2 == 0)
        {
            CreateLeftBlock(leftPosition);
            CreateRightBlock(rightPosition);
        }
        else
        {
            CreateTopBlock(topPosition);
            CreateBottomBlock(bottomPosition);
        }
    }

    private void CreateLeftBlock(Vector3 position)
    { // 이전에 5번, 9번 노드에 고정적으로 생성하던 방식과 똑같음
        GameObject newBlock = Instantiate(barPrefab);

        newBlock.name = "leftNewBlock";
        newBlock.transform.localScale = new Vector3(4, 5, 2);
        newBlock.transform.position = position;
    }
    private void CreateTopBlock(Vector3 position)
    { // 상단과 하단 장애물은 복도에 맞게 방향을 바꿔주었음, 귀찮아서 rotate 안하고 그냥 scale 바꿈...
        GameObject newBlock = Instantiate(barPrefab);

        newBlock.name = "topNewBlock";
        newBlock.transform.localScale = new Vector3(2, 5, 4);
        newBlock.transform.position = position;
    }
    private void CreateRightBlock(Vector3 position)
    {
        GameObject newBlock = Instantiate(barPrefab);

        newBlock.name = "rightNewBlock";
        newBlock.transform.localScale = new Vector3(4, 5, 2);
        newBlock.transform.position = position;
    }
    private void CreateBottomBlock(Vector3 position)
    {
        GameObject newBlock = Instantiate(barPrefab);

        newBlock.name = "bottomNewBlock";
        newBlock.transform.localScale = new Vector3(2, 5, 4);
        newBlock.transform.position = position;
    }
    /////////////////////////////////
    */
    ////////// CCTV 5, 6번 위치 변수 //////////
    private void CreateCameraPosition()
    {
        camera_p = nodeList[1];
    }
    /////////////////////////////////


}