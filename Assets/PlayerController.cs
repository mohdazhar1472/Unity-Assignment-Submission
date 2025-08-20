using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Pathfinding pathfinding;
    public GridManager gridManager;
    public ObstacleData obstacleData;

    private List<Tile> path;
    private bool isMoving = false;
    public float moveSpeed = 3f;

    void Update()
    {
        if (isMoving)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Tile targetTile = hit.collider.GetComponent<Tile>();
                if (targetTile != null && !obstacleData.obstacles[targetTile.gridZ * 10 + targetTile.gridX])
                {
                    Tile startTile = gridManager.grid[(int)transform.position.x, (int)transform.position.z];
                    path = pathfinding.FindPath(startTile, targetTile);
                    if (path != null)
                    {
                        StartCoroutine(MoveAlongPath());
                    }
                }
            }
        }
    }

    IEnumerator MoveAlongPath()
    {
        isMoving = true;
        foreach (var tile in path)
        {
            Vector3 targetPos = new Vector3(tile.gridX, 0, tile.gridZ);
            while (Vector3.Distance(transform.position, targetPos) > 0.05f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
        isMoving = false;

    }
   void Awake()
{
    if (pathfinding == null)
        pathfinding = FindObjectOfType<Pathfinding>();

    if (gridManager == null)
        gridManager = FindObjectOfType<GridManager>();

    if (obstacleData == null)
        obstacleData = Resources.Load<ObstacleData>("ObstacleData");  // Place your asset in Resources folder
}

}

