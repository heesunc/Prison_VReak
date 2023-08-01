using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] Vector3Int mapSize; //원하는 맵의 크기
    [SerializeField] float minimumDivideRate; //공간이 나눠지는 최소 비율
    [SerializeField] float maximumDivideRate; //공간이 나눠지는 최대 비율
    [SerializeField] private int maximumDepth; //트리의 높이, 높을 수록 방을 더 자세히 나누게 됨
    [SerializeField] private GameObject wallPrefab; // 방 크기에 맞춰 벽생
    [SerializeField] private GameObject barPrefab;
    [SerializeField] private GameObject puzzlebtn1; // 퍼즐버튼1
    [SerializeField] private GameObject puzzlebtn2; // 퍼즐버튼2
    [SerializeField] private int num = 1;
    [SerializeField] public int a;
    [SerializeField] public int b;
    [SerializeField]private List<GameObject> wallList = new List<GameObject>();
    //[SerializeField] private GameObject outsidewall; //외벽 생성
    //[SerializeField] public float wallSpacing = 0.1f;
    //[SerializeField] public int wallCounter = 1;

    private int wallPrefabCounter = 1; // prefab에 번호를 붙이기 위해 만듬

    void Start()
    {
        Node root = new Node(new RectInt(0, 0, mapSize.x, mapSize.y)); //전체 맵 크기의 루트노드를 만듬
        Divide(root, 0);
        GenerateRoom(root, 0);

        puzzleBtn();
        
        // wallPrefab9 및 wallPrefab5의 Transform 컴포넌트를 가져옵니다.
        Transform wallPrefab9Transform = GameObject.Find("wallPrefab9").transform;
        Transform wallPrefab5Transform = GameObject.Find("wallPrefab5").transform;

        // wallPrefab9의 하단 위치를 계산합니다.
        Vector3 wallPrefab9BottomPosition = new Vector3(
            wallPrefab9Transform.position.x,
            wallPrefab9Transform.position.y,
            wallPrefab9Transform.position.z - (wallPrefab9Transform.localScale.z / 2.0f)-1.0f
        );

        // wallPrefab5의 좌측 위치를 계산합니다.
        Vector3 wallPrefab5LeftPosition = new Vector3(
            wallPrefab5Transform.position.x - (wallPrefab5Transform.localScale.x / 2.0f)-1.0f,
            wallPrefab5Transform.position.y,
            wallPrefab5Transform.position.z
        );

        // 값을 콘솔창에 출력
        Debug.Log("wallPrefab9BottomPosition: " + wallPrefab9BottomPosition);
        Debug.Log("wallPrefab5LeftPosition: " + wallPrefab5LeftPosition);
    
        // 계산된 위치에 새로운 구조물을 생성합니다.
        CreateNewBlock9(wallPrefab9BottomPosition);
        CreateNewBlock5(wallPrefab5LeftPosition);
    }


    void Divide(Node tree, int n)
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
        Divide(tree.leftNode, n + 1); //왼쪽, 오른쪽 자식 노드들도 나눠준다.
        Divide(tree.rightNode, n + 1);//왼쪽, 오른쪽 자식 노드들도 나눠준다.
    }

    private RectInt GenerateRoom(Node tree, int n)
    {
        RectInt rect;
        if (n == maximumDepth) //해당 노드가 리프노드라면 방을 만들어 줄 것이다.
        {
            rect = tree.nodeRect;
            int width = Mathf.Max(rect.width - 4, 3);
            //방의 가로 최소 크기는 노드의 가로길이의 절반, 최대 크기는 가로길이보다 1 작게 설정한 후 그 사이 값중 랜덤한 값을 구해준다.
            int height = Mathf.Max(rect.height - 4, 3);
            //높이도 위와 같다.
            int x = rect.x + Random.Range(2, rect.width - width - 2);
            //방의 x좌표이다. 만약 0이 된다면 붙어 있는 방과 합쳐지기 때문에,최솟값은 1로 해주고, 최댓값은 기존 노드의 가로에서 방의 가로길이를 빼 준 값이다.
            int y = rect.y + Random.Range(2, rect.height - height - 2);
            //y좌표도 위와 같다.
            rect = new RectInt(x, y, width, height);
            GameObject floor = GameObject.Find("Plane");
            
            
            //CreateoutWall();
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

    // 내부 구조물 생성
    private void CreateWalls(RectInt rect)
    {
        // wallPrefab을 사용하여 벽을 만듭니다.
        GameObject Wall = Instantiate(wallPrefab);
        Wall.transform.localScale = new Vector3(rect.width-1, 5, rect.height-1);
        Wall.transform.position = new Vector3(rect.x + rect.width / 2f, 2.5f, rect.y + rect.height / 2f);
        
        string wallName = "wallPrefab" + wallPrefabCounter;
        Wall.name = wallName;
        num++;
        wallPrefabCounter++;
        wallList.Add(Wall);
    }


    // 장애물 생성
    private void CreateNewBlock5(Vector3 position)
    {
        GameObject newBlock = Instantiate(barPrefab);

        newBlock.name = "newBlock5";

        newBlock.transform.localScale = new Vector3(2, 5, 2);
        newBlock.transform.position = position;
    }
    private void CreateNewBlock9(Vector3 position)
    {
        GameObject newBlock = Instantiate(barPrefab);

        newBlock.name = "newBlock9";

        newBlock.transform.localScale = new Vector3(2, 5, 2);
        newBlock.transform.position = position;
    }

    // 퍼즐 위치 생성
    private void selectNum()
    {
        System.Random random = new System.Random();
        a = random.Next(1, 16);
        b = random.Next(1, 16);
        while(a == b)
        {
            b = random.Next(1, 16);
        }
    }
    private void puzzleBtn()
    {
        selectNum();
        foreach (GameObject Wall in wallList)
        {
            if (Wall.name == wallPrefab.name + a) // wallPrefab의 이름에 (Clone)과 a를 더한 이름과 비교
            {
                Vector3 wall1_p = Wall.transform.position;
                Vector3 wall1_s = Wall.transform.localScale;
                GameObject btn1 = Instantiate(puzzlebtn1);
                btn1.transform.position = new Vector3(wall1_p.x, 1.0f, wall1_p.z+wall1_s.z * 0.5f);
                Debug.Log("버튼1번의 포지션 : " + btn1.transform.position);
                Debug.Log("버튼 1번이 설치된 벽의 포지션 : " + Wall.transform.position);
                Debug.Log("버튼 1번이 설치된 벽의 스케일 : " + Wall.transform.localScale);
                Debug.Log("설치된 번호 표시 : " + Wall.name + "에 a버튼 생성");
            }
            else if (Wall.name == wallPrefab.name + b) // wallPrefab의 이름에 (Clone)과 b를 더한 이름과 비교
            {
                Vector3 wall2_p = Wall.transform.position;
                Vector3 wall2_s = Wall.transform.localScale;
                GameObject btn2 = Instantiate(puzzlebtn2);
                btn2.transform.position = new Vector3(wall2_p.x, 1.0f, wall2_p.z+wall2_s.z * 0.5f);
                Debug.Log("버튼2번의 포지션 : " + btn2.transform.position);
                Debug.Log("버튼 2번이 설치된 벽의 포지션 : " + Wall.transform.position);
                Debug.Log("버튼 2번이 설치된 벽의 스케일 : " + Wall.transform.localScale);
                Debug.Log("설치된 번호 표시 : " + Wall.name + "에 b버튼 생성");
            }
        }
    }


    //private void CreateoutWall()
    //{

    //    GameObject outwall = GameObject.Find("outsidewall");
    //    outwall.transform.localScale = new Vector3(1, 8, 1);
    //    for (int x = 0; x < mapSize.x; x++)
    //    {
    //        for (int y = 0; y < mapSize.y; y++)
    //        {
    //            if (x == 0 || x == mapSize.x - 1 || y == 0 || y == mapSize.y - 1)
    //            {
    //                // 테두리 벽 생성
    //                Vector3 position = new Vector3(x + wallSpacing, 0, y + wallSpacing);
    //                GameObject newWall = Instantiate(outwall, position, Quaternion.identity);

    //                wallCounter++;
    //            }
    //        }
    //    }
    //}
}
    