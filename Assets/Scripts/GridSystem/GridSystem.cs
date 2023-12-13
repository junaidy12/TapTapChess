using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridSystem
{
    private int width;
    private int height;
    private float cellSize;
    Dictionary<Vector2Int,GridObject> gridObjectDic = new Dictionary<Vector2Int, GridObject>();

    public GridSystem(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                gridObjectDic.Add(new Vector2Int(x,y),new GridObject(new Vector2Int(x, y), false));
            }
        }
    }
    public Vector3 GetWorldPosition(Vector2Int gridPosition)
    {
        return new Vector3(gridPosition.x, gridPosition.y, 0) * cellSize;
    }

    public Vector2Int GetGridPosition(Vector2 worldPosition)
    {
        return new Vector2Int(
            Mathf.RoundToInt(worldPosition.x / cellSize),
            Mathf.RoundToInt(worldPosition.y / cellSize)
        );
    }

    public GridObject GetGridObject(Vector2Int gridPosition)
    {
        if (gridObjectDic.ContainsKey(gridPosition))
        {
            return gridObjectDic[gridPosition];
        }
        return null;
    }

    public List<GridObject> GridObjectSimilar(Block block)
    {
        List<GridObject> gridObject = new List<GridObject>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2Int gridPos = new Vector2Int(x, y);
                if(gridObjectDic[gridPos].block == block)
                {
                    gridObject.Add(gridObjectDic[gridPos]);
                }
            }
        }
        return gridObject;
    }

    public void ResetGridObjectList()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2Int gridPos = new Vector2Int(x, y);
                gridObjectDic[gridPos].UpdateBlock(BlockPlacer.Instance.GetDefaultBlock());
                gridObjectDic[gridPos].ResetBlock();
                Transform gridObjectObject = gridObjectDic[gridPos].gridObject;
                gridObjectObject.GetComponent<GridObjectVisual>().HideAttackSprite();
                gridObjectObject.GetComponent<GridObjectVisual>().HideBlockSprite();
            }
        }
    }


    public void CreateGridDebugVisual(Transform debugObject, Transform parent)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2Int gridPos = new Vector2Int(x, y);
                Vector2Int gridPosition = gridObjectDic[gridPos].gridPosition;
                Transform debugTransform = GameObject.Instantiate(debugObject, GetWorldPosition(gridPosition), Quaternion.identity, parent);
                GridObjectVisual gridObjectVisual = debugTransform.GetComponent<GridObjectVisual>();
                gridObjectDic[gridPos].SetObject(gridObjectVisual.transform);
                gridObjectVisual.SetupGridVisual(GetGridObject(gridPosition));
            }
        }
    }
}
