
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridObject
{
    public Block block { get; private set; }
    
    public Vector2Int gridPosition { get; private set; }

    public bool IsClicked { get { return isClicked; } private set { isClicked = false; } }
    bool isClicked = false;


    public GridObject(Vector2Int gridPosition, bool isClicked)
    {
        this.gridPosition = gridPosition;
        this.isClicked = isClicked;
    }

    public void UpdateBlock(Block sprite, bool state)
    {
        this.block = sprite;

        isClicked = state;
    }
}
