using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridSystem
{
    private int width;
    private int height;
    private float cellSize;
    GridObject[,] gridObjectArray;

    public GridSystem(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        gridObjectArray = new GridObject[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                gridObjectArray[x, y] = new GridObject(new Vector2Int(x, y), false);
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
        return gridObjectArray[gridPosition.x, gridPosition.y];
    }

    public List<GridObject> GridObjectSimilar(Block block)
    {
        List<GridObject> gridObject = new List<GridObject>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(gridObjectArray[x,y].block == block)
                {
                    gridObject.Add(gridObjectArray[x, y]);
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
                gridObjectArray[x, y].UpdateBlock(BlockPlacer.Instance.GetDefaultBlock(), false);
            }
        }
    }


    public void CreateGridDebugVisual(Transform debugObject, Transform parent)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2Int gridPosition = gridObjectArray[x, y].gridPosition;
                Transform debugTransform = GameObject.Instantiate(debugObject, GetWorldPosition(gridPosition), Quaternion.identity, parent);
                GridObjectVisual gridObjectVisual = debugTransform.GetComponent<GridObjectVisual>();
                gridObjectVisual.SetupGridVisual(GetGridObject(gridPosition));
            }
        }
    }
}
