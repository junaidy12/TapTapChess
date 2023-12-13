using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBlock : Block
{
    public override List<Vector2Int> AttackGridPosition(Vector2Int gridPosition, Vector2Int gridSize)
    {
        List<Vector2Int> gridPositionList = new List<Vector2Int>();

        //Kanan Atas
        int x = gridPosition.x + 1;
        int y = gridPosition.y + 2;
        if(x < gridSize.x && y < gridSize.y)
        {
            gridPositionList.Add(new Vector2Int(x, y));
        }
        x = gridPosition.x + 2;
        y = gridPosition.y + 1;
        if (x < gridSize.x && y < gridSize.y)
        {
            gridPositionList.Add(new Vector2Int(x, y));
        }

        //Kiri Atas
        x = gridPosition.x - 2;
        y = gridPosition.y + 1;
        if (x >= 0 && y < gridSize.y)
        {
            gridPositionList.Add(new Vector2Int(x, y));
        }
        x = gridPosition.x - 1;
        y = gridPosition.y + 2;
        if (x >= 0 && y < gridSize.y)
        {
            gridPositionList.Add(new Vector2Int(x, y));
        }

        //Kiri Bawah
        x = gridPosition.x - 1;
        y = gridPosition.y - 2;
        if (x >= 0 && y >= 0)
        {
            gridPositionList.Add(new Vector2Int(x, y));
        }
        x = gridPosition.x - 2;
        y = gridPosition.y - 1;
        if (x >= 0 && y >= 0)
        {
            gridPositionList.Add(new Vector2Int(x, y));
        }

        //Kanan Bawah
        x = gridPosition.x + 1;
        y = gridPosition.y - 2;
        if (x < gridSize.x && y >= 0)
        {
            gridPositionList.Add(new Vector2Int(x, y));
        }
        x = gridPosition.x + 2;
        y = gridPosition.y - 1;
        if (x < gridSize.x && y >= 0)
        {
            gridPositionList.Add(new Vector2Int(x, y));
        }



        return gridPositionList;
    }
}
