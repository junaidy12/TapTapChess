using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RookBlock : Block
{
    public override List<Vector2Int> AttackGridPosition(Vector2Int gridPosition, Vector2Int gridSize)
    {
        List<Vector2Int> gridPositionList = new List<Vector2Int>();

        //Kanan
        for (int x = gridPosition.x + 1; x < gridSize.x; x++)
        {
            gridPositionList.Add(new Vector2Int(x, gridPosition.y));
        }

        //Kiri
        for (int x = gridPosition.x - 1; x >= 0; x--)
        {
            gridPositionList.Add(new Vector2Int(x, gridPosition.y));
        }

        //Atas
        for (int y = gridPosition.y + 1; y < gridSize.y; y++)
        {
            gridPositionList.Add(new Vector2Int(gridPosition.x, y));
        }

        //Bawah
        for (int y = gridPosition.y-1; y >= 0; y--)
        {
            gridPositionList.Add(new Vector2Int(gridPosition.x, y));
        }

        return gridPositionList;
    }
}
