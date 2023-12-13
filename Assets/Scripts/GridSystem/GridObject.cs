
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridObject
{
    public Block block { get; private set; }
    
    public Vector2Int gridPosition { get; private set; }

    public Transform gridObject { get; private set; }

    public bool IsPlaced { get { return isPlaced; } private set { isPlaced = false; } }
    [SerializeField] bool isPlaced = false;


    public GridObject(Vector2Int gridPosition, bool isClicked)
    {
        this.gridPosition = gridPosition;
        this.isPlaced = isClicked;
    }

    public void UpdateBlock(Block sprite)
    {
        this.block = sprite;

        isPlaced = true;
    }

    public void ResetBlock()
    {
        isPlaced = false;
    }

    public void SetObject(Transform transform)
    {
        gridObject = transform;
    }
}
