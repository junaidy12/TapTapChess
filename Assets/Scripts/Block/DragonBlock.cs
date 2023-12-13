using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBlock : Block
{
    public override List<Vector2Int> AttackGridPosition(Vector2Int gridPosition, Vector2Int gridSize)
    {
        List<Vector2Int> gridPositionList = new List<Vector2Int>();

        int x = gridPosition.x + 1;

        //Kanan
        if(x < gridSize.x)
        {
            gridPositionList.Add(new Vector2Int(x, gridPosition.y));
            //Kanan Atas
            if(gridPosition.y + 1 < gridSize.y)
            {
                gridPositionList.Add(new Vector2Int(x, gridPosition.y + 1));
            }
            //Kanan Bawah
            if (gridPosition.y - 1 >= 0)
            {
                gridPositionList.Add(new Vector2Int(x, gridPosition.y - 1));
            }
        }

        //Kiri
        x = gridPosition.x - 1;
        if(x >= 0)
        {
            gridPositionList.Add(new Vector2Int(x, gridPosition.y));
            //Kiri Atas
            if(gridPosition.y + 1 < gridSize.y)
            {
                gridPositionList.Add(new Vector2Int(x, gridPosition.y + 1));
            }
            //Kiri Bawah
            if (gridPosition.y - 1 >= 0)
            {
                gridPositionList.Add(new Vector2Int(x, gridPosition.y - 1));
            }
        }

        //Atas
        if(gridPosition.y + 1 < gridSize.y)
        {
            gridPositionList.Add(new Vector2Int(gridPosition.x, gridPosition.y + 1));
        }

        //Bawah
        if (gridPosition.y - 1 >= 0)
        {
            gridPositionList.Add(new Vector2Int(gridPosition.x, gridPosition.y - 1));
        }

        return gridPositionList;
    }
}
