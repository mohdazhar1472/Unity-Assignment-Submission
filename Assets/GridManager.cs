using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject tilePrefab; // Assign TilePrefab here
    public int width = 10;
    public int height = 10;

    [HideInInspector]
    public Tile[,] grid;

    void Start()
    {
        grid = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 pos = new Vector3(x, 0, z);
                GameObject tileObj = Instantiate(tilePrefab, pos, Quaternion.identity, transform);
                tileObj.name = $"Tile_{x}_{z}";

                Tile tile = tileObj.AddComponent<Tile>();
                tile.gridX = x;
                tile.gridZ = z;

                grid[x, z] = tile;
            }
        }
    }
}
