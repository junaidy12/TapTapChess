using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [Tooltip("Test, Delete After")]
    public bool include;

    public Sprite sprite;
    public BlockType blockType;
    public int score;

    public enum BlockType
    {
        Default,
        Rook,
        Bishop,
        Knight,
        Dragon
    }

    public virtual List<Vector2Int> CheckUnvalidGridPosition(Vector2Int gridPosition, Vector2Int gridSize)
    {
        List<Vector2Int> gridPositionList = new List<Vector2Int>();

        return gridPositionList;
    }

    public void DeleteThisObject()
    {
        Destroy(gameObject);
    }
}
