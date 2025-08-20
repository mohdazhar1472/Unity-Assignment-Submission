using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleData", menuName = "CrimsonTactics/ObstacleData")]
public class ObstacleData : ScriptableObject
{
    // Flattened 10x10 grid, true = obstacle, false = free
    public bool[] obstacles = new bool[100];
}

