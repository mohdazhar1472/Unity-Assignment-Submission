using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObstacleEditor : EditorWindow
{
    private ObstacleData obstacleData;
    private const int gridSize = 10;

    [MenuItem("Tools/Obstacle Editor")]
    public static void ShowWindow()
    {
        GetWindow<ObstacleEditor>("Obstacle Editor");
    }

    private void OnGUI()
    {
        obstacleData = (ObstacleData)EditorGUILayout.ObjectField("Obstacle Data", obstacleData, typeof(ObstacleData), false);

        if (obstacleData == null)
        {
            EditorGUILayout.HelpBox("Please assign an ObstacleData asset.", MessageType.Warning);
            return;
        }

        GUILayout.Label("Toggle Obstacle Tiles", EditorStyles.boldLabel);

        for (int z = 0; z < gridSize; z++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < gridSize; x++)
            {
                int index = z * gridSize + x;
                obstacleData.obstacles[index] = GUILayout.Toggle(obstacleData.obstacles[index], "");
            }
            EditorGUILayout.EndHorizontal();
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(obstacleData);
        }
    }
}

