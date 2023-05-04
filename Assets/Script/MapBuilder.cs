using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class MapBuilder : MonoBehaviour
{
    public GameObject wallPrefab; // Reference to your wall prefab
    public GameObject planePrefab; // Reference to your plane prefab
    public GameObject outsideWallPrefab; // Reference to your outside wall prefab

    void Start()
    {
        // Instantiate your map prefabs and position them
        GameObject wall = Instantiate(wallPrefab, transform.position, Quaternion.identity);
        GameObject plane = Instantiate(planePrefab, transform.position, Quaternion.identity);
        GameObject outsideWall = Instantiate(outsideWallPrefab, transform.position, Quaternion.identity);

        // Collect the MeshFilter components of your map objects
        MeshFilter[] wallMeshFilters = wall.GetComponentsInChildren<MeshFilter>();
        MeshFilter[] planeMeshFilters = plane.GetComponentsInChildren<MeshFilter>();
        MeshFilter[] outsideWallMeshFilters = outsideWall.GetComponentsInChildren<MeshFilter>();

        // Create a NavMeshBuildSettings object with your desired settings
        NavMeshBuildSettings settings = NavMesh.CreateSettings();
        settings.agentRadius = 0.5f;
        settings.agentHeight = 2.0f;
        settings.agentSlope = 45.0f;

        // Create a NavMeshData object to store the baked NavMesh
        NavMeshData navMeshData = new NavMeshData();

        // Collect the sources for the NavMesh build
        List<NavMeshBuildSource> sources = new List<NavMeshBuildSource>();

        // Add the wall MeshFilters to the sources list
        foreach (MeshFilter meshFilter in wallMeshFilters)
        {
            sources.Add(new NavMeshBuildSource
            {
                shape = NavMeshBuildSourceShape.Mesh,
                sourceObject = meshFilter.sharedMesh,
                transform = meshFilter.transform.localToWorldMatrix,
                area = NavMesh.GetAreaFromName("Walkable")
            });
        }

        // Add the plane MeshFilters to the sources list
        foreach (MeshFilter meshFilter in planeMeshFilters)
        {
            sources.Add(new NavMeshBuildSource
            {
                shape = NavMeshBuildSourceShape.Mesh,
                sourceObject = meshFilter.sharedMesh,
                transform = meshFilter.transform.localToWorldMatrix,
                area = NavMesh.GetAreaFromName("Walkable")
            });
        }

        // Add the outside wall MeshFilters to the sources list
        foreach (MeshFilter meshFilter in outsideWallMeshFilters)
        {
            sources.Add(new NavMeshBuildSource
            {
                shape = NavMeshBuildSourceShape.Mesh,
                sourceObject = meshFilter.sharedMesh,
                transform = meshFilter.transform.localToWorldMatrix,
                area = NavMesh.GetAreaFromName("Not Walkable")
            });
        }

//         // Initialize NavMeshData
//     NavMeshData navMeshData = new NavMeshData();
//     NavMesh.AddNavMeshData(navMeshData);

//     // Build the NavMesh
//     NavMeshBuilder.BuildNavMeshAsync(settings, sources, new Bounds(transform.position, Vector3.one * 1000), navMeshData).completed += OnNavMeshBuildComplete;    }
    }
//     void OnNavMeshBuildComplete(AsyncOperation<NavMeshData> obj)
// {
//     // Assign the baked NavMesh to your NavMesh object
//     NavMesh navMesh = NavMeshObject.GetComponent<NavMesh>();
//     navMesh.RemoveNavMeshData(navMesh.navMeshData);
//     navMesh.AddNavMeshData(obj.Result);
// }
}
