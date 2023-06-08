using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    //SINGLETON
    public static LevelGrid Instance;


    //PROPERTY
    [SerializeField] Transform debugObject;
    [SerializeField] Vector2Int gridSize;

    private GridSystem gridSystem;
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        gridSystem = new GridSystem(gridSize.x, gridSize.y, 1);
        gridSystem.CreateGridDebugVisual(debugObject, transform);
    }


    public bool SetGridObjectVisual(Block block, Vector2Int gridPosition, bool state)
    {
        if (gridPosition.x >= gridSize.x || gridPosition.y >= gridSize.y || gridPosition.x < 0 || gridPosition.y < 0) return false;
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        if (gridObject.IsClicked) return false;
        gridObject.UpdateBlock(block, state);
        return true;
    }

    public Vector2Int GetGridSize()
    {
        return gridSize;
    }

    public void ResetLevel()
    {
        gridSystem.ResetGridObjectList();
        BlockPlacer.Instance.ResetBlockAndScore();
    }

    // Pass Through Method to GridSystem class
    public GridObject GetGridObject(Vector2Int gridPosition) => gridSystem.GetGridObject(gridPosition);
    public Vector2Int GetGridPosition(Vector3 position) => gridSystem.GetGridPosition(position);
    public Vector3 GetWorldPosition(Vector2Int gridPosition) => gridSystem.GetWorldPosition(gridPosition);
    public List<GridObject> GetGridObjectSimilar(Block block) => gridSystem.GridObjectSimilar(block);
    public void ExitGame()
    {
        Application.Quit();
    }
}
