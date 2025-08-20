using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int gridX;
    public int gridZ;

    private Renderer tileRenderer;
    private Color originalColor;

    void Start()
    {
        tileRenderer = GetComponent<Renderer>();
        originalColor = tileRenderer.material.color;
    }

    public void Highlight()
    {
        tileRenderer.material.color = Color.yellow;
    }

    public void ResetColor()
    {
        tileRenderer.material.color = originalColor;
    }
}
