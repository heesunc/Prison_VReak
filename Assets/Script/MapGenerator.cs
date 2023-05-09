using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
//d
public class MapGenerator : MonoBehaviour
{
    [SerializeField] Vector3Int mapSize; //원하는 맵의 크기
    [SerializeField] float minimumDevideRate; //공간이 나눠지는 최소 비율
    [SerializeField] float maximumDivideRate; //공간이 나눠지는 최대 비율
    [SerializeField] private int maximumDepth; //트리의 높이, 높을 수록 방을 더 자세히 나누게 됨
    [SerializeField] private GameObject wallPrefeb; // 방 크기에 맞춰 벽생
    //[SerializeField] private GameObject Plane; //바닥
    [SerializeField] private GameObject outsidewall; //외벽 생성
    [SerializeField] public float wallSpacing = 0.1f;
    [SerializeField] public int wallCounter = 1;
    void Start()
    {
        Node root = new Node(new RectInt(0, 0, mapSize.x, mapSize.y)); //전체 맵 크기의 루트노드를 만듬
        //CreateoutWall();
        Divide(root, 0);
        GenerateRoom(root, 0);

    }
    void Divide(Node tree, int n)
    {
        if (n == maximumDepth) return; //내가 원하는 높이에 도달하면 더 나눠주지 않는다.

        //그 외의 경우에는

        int maxLength = Mathf.Max(tree.nodeRect.width, tree.nodeRect.height);
        //가로와 세로중 더 긴것을 구한후, 가로가 길다면 위 좌, 우로 세로가 더 길다면 위, 아래로 나눠주게 될 것이다.
        int split = Mathf.RoundToInt(Random.Range(maxLength * minimumDevideRate, maxLength * maximumDivideRate));
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
            int width = rect.width - 4;
            //방의 가로 최소 크기는 노드의 가로길이의 절반, 최대 크기는 가로길이보다 1 작게 설정한 후 그 사이 값중 랜덤한 값을 구해준다.
            int height = rect.height - 4;
            //높이도 위와 같다.
            int x = rect.x + Random.Range(2, rect.width - width - 2);
            //방의 x좌표이다. 만약 0이 된다면 붙어 있는 방과 합쳐지기 때문에,최솟값은 1로 해주고, 최댓값은 기존 노드의 가로에서 방의 가로길이를 빼 준 값이다.
            int y = rect.y + Random.Range(2, rect.height - height - 2);
            //y좌표도 위와 같다.
            rect = new RectInt(x, y, width, height);
            GameObject floor = GameObject.Find("Plane");
            
            
            //floor.transform.localScale = new Vector3(mapSize.x/10, 1, mapSize.y/10);
            //floor.transform.position = new Vector3(mapSize.x/2, 0, mapSize.y/2);
            CreateoutWall();
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
        GameObject Wall = Instantiate(wallPrefeb);
        Wall.transform.localScale = new Vector3(rect.width-2, 5, rect.height-2);
        Wall.transform.position = new Vector3(rect.x + rect.width / 2f, 2, rect.y + rect.height / 2f);

    }
    private void CreateoutWall()
    {
        //GameObject floor = GameObject.Find("Plane");
        GameObject outwall = GameObject.Find("outsidewall");
        outwall.transform.localScale = new Vector3(1, 8, 1);
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                if (x == 0 || x == mapSize.x - 1 || y == 0 || y == mapSize.y - 1)
                {
                    // 테두리 벽 생성
                    Vector3 position = new Vector3(x + wallSpacing, 0, y + wallSpacing);
                    GameObject newWall = Instantiate(outwall, position, Quaternion.identity);

                    wallCounter++;
                }
            }
        }
    }
}
    