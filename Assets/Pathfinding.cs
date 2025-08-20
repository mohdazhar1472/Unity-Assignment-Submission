using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public Tile[,] grid; // Reference to your grid tiles
    public ObstacleData obstacleData;
    public int gridWidth = 10;
    public int gridHeight = 10;

    public List<Tile> FindPath(Tile start, Tile target)
    {
        var openSet = new List<Tile> { start };
        var closedSet = new HashSet<Tile>();
        var cameFrom = new Dictionary<Tile, Tile>();

        var gScore = new Dictionary<Tile, int> {{start, 0}};
        var fScore = new Dictionary<Tile, int> {{start, Heuristic(start, target)}};

        while (openSet.Count > 0)
        {
            Tile current = null;
            int lowestF = int.MaxValue;

            foreach (Tile node in openSet)
            {
                if (!fScore.ContainsKey(node)) continue;
                if (fScore[node] < lowestF)
                {
                    lowestF = fScore[node];
                    current = node;
                }
            }

            if (current == target)
                return ReconstructPath(cameFrom, current);

            openSet.Remove(current);
            closedSet.Add(current);

            foreach (Tile neighbor in GetNeighbors(current))
            {
                if (closedSet.Contains(neighbor) || IsObstacle(neighbor))
                    continue;

                int tentativeG = gScore[current] + 1;

                if (!gScore.ContainsKey(neighbor) || tentativeG < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeG;
                    fScore[neighbor] = tentativeG + Heuristic(neighbor, target);

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

        return null; // No path found
    }

    int Heuristic(Tile a, Tile b)
    {
        return Mathf.Abs(a.gridX - b.gridX) + Mathf.Abs(a.gridZ - b.gridZ);
    }

    List<Tile> ReconstructPath(Dictionary<Tile, Tile> cameFrom, Tile current)
    {
        var path = new List<Tile> { current };
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Add(current);
        }
        path.Reverse();
        return path;
    }

    bool IsObstacle(Tile tile)
    {
        int index = tile.gridZ * gridWidth + tile.gridX;
        return obstacleData != null && obstacleData.obstacles[index];
    }

    List<Tile> GetNeighbors(Tile tile)
    {
        var neighbors = new List<Tile>();

        int x = tile.gridX;
        int z = tile.gridZ;

        if (x > 0) neighbors.Add(grid[x - 1, z]);
        if (x < gridWidth - 1) neighbors.Add(grid[x + 1, z]);
        if (z > 0) neighbors.Add(grid[x, z - 1]);
        if (z < gridHeight - 1) neighbors.Add(grid[x, z + 1]);

        return neighbors;
    }
}

