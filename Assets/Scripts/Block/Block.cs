using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [Tooltip("Test for debug, check if you want to include this block into randomizer")]
    public bool include;

    [Header("Parameter")]
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

    public virtual List<Vector2Int> AttackGridPosition(Vector2Int gridPosition, Vector2Int gridSize)
    {
        List<Vector2Int> gridPositionList = new List<Vector2Int>();

        return gridPositionList;
    }

    public void DeleteThisObject()
    {
        Destroy(gameObject);
    }
}
