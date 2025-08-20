using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileRaycaster : MonoBehaviour
{
    public TMP_Text positionText;

    private Tile lastHoveredTile;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Tile tile = hit.collider.GetComponent<Tile>();
            if (tile != null)
            {
                // Reset color of previous tile
                if (lastHoveredTile != null && lastHoveredTile != tile)
                {
                    lastHoveredTile.ResetColor();
                }

                // Highlight current tile
                tile.Highlight();
                lastHoveredTile = tile;

                // Display position on TextMeshPro UI
                positionText.text = $"Tile Position: X={tile.gridX}, Z={tile.gridZ}";
                return;
            }
        }

        // If no tile hit, reset previous tile color and clear text
        if (lastHoveredTile != null)
        {
            lastHoveredTile.ResetColor();
            lastHoveredTile = null;
        }
        positionText.text = "";
    }
}


