using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopBlock : Block
{
    public override List<Vector2Int> AttackGridPosition(Vector2Int gridPosition, Vector2Int gridSize)
    {
        List<Vector2Int> gridPositionList = new List<Vector2Int>();
        
        //Kanan Atas
        for (int x = gridPosition.x + 1, y = gridPosition.y + 1; x < gridSize.x && y < gridSize.y; x++, y++){
            gridPositionList.Add(new Vector2Int(x, y));
        }

        //Kiri Atas
        for (int x = gridPosition.x - 1, y = gridPosition.y + 1; x >= 0 && y < gridSize.y; x--, y++)
        {
            gridPositionList.Add(new Vector2Int(x, y));
        }

        //Kanan Bawah
        for (int x = gridPosition.x + 1, y = gridPosition.y - 1; x < gridSize.x && y >= 0; x++, y--)
        {
            gridPositionList.Add(new Vector2Int(x, y));
        }

        //Kiri Bawah
        for (int x = gridPosition.x - 1, y = gridPosition.y - 1; x >= 0 && y >= 0; x--, y--)
        {
            gridPositionList.Add(new Vector2Int(x, y));
        }

        return gridPositionList;
    }
}
