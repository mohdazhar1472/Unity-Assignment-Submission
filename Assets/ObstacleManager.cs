using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public ObstacleData obstacleData;     // Reference to your ObstacleData asset
    public GameObject obstaclePrefab;     // Assign a red sphere prefab here

    private GameObject[,] obstaclesVisuals;
    private int gridSizeX = 10;
    private int gridSizeZ = 10;

    void Start()
    {
        obstaclesVisuals = new GameObject[gridSizeX, gridSizeZ];

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                int index = z * gridSizeX + x;
                if (obstacleData.obstacles[index])
                {
                    Vector3 pos = new Vector3(x, 0.5f, z); // Place sphere above tile
                    GameObject obs = Instantiate(obstaclePrefab, pos, Quaternion.identity, transform);
                    obstaclesVisuals[x, z] = obs;
                }
            }
        }
    }
}

