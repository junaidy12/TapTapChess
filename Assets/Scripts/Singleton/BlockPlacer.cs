using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockPlacer : MonoBehaviour
{
    //SINGLETON
    public static BlockPlacer Instance;

    //EVENT
    public event EventHandler<OnBlockClickedEventArgs> OnBlockClicked;

    //EVENT VALUE
    public class OnBlockClickedEventArgs : EventArgs
    {
        public int score;
    }


    [SerializeField] BlockRandomizer blockRandomizer;
    [SerializeField] Block defaultBlock;
    [SerializeField] Block attackBlock;
    [SerializeField] float placeTime;

    int score;
    bool gameOver = false;
    float timer;
    Vector2Int lastGridPosition;
    List<Vector2Int> lastAttackPlaceholder = new List<Vector2Int>();
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        timer = placeTime;
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (!gameOver)
        {
            timer -= Time.deltaTime;
            if (timer < 0) gameOver = true;
            Vector2Int gridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetMousePosition());
            List<Vector2Int> attackGridPosition = blockRandomizer.GetSelectedBlock().AttackGridPosition(gridPosition, LevelGrid.Instance.GetGridSize());
            lastAttackPlaceholder = ShowPlaceHolder(gridPosition, attackGridPosition);
            PlaceBlock(gridPosition, attackGridPosition);
        }
    }
    List<Vector2Int> ShowPlaceHolder(Vector2Int gridPosition, List<Vector2Int> attackGridPosition)
    {
        List<Vector2Int> attackPlaceholderPos = new List<Vector2Int>();

        GridObject gridObject = LevelGrid.Instance.GetGridObject(gridPosition);
        GridObject tempGridObject = LevelGrid.Instance.GetGridObject(lastGridPosition);

        Transform tempObject = tempGridObject.gridObject;
        List<Vector2Int> lastGridAttackPosition = blockRandomizer.GetSelectedBlock().AttackGridPosition(lastGridPosition, LevelGrid.Instance.GetGridSize());

        //Check if mouse outside the play area
        if (LevelGrid.Instance.IsPositionOutsideGrid(gridPosition, out Vector2Int gridPos))
        {
            tempObject.GetComponent<GridObjectVisual>().HideBlockSprite();
            attackPlaceholderPos = HideAttackPlaceholder(lastGridAttackPosition);
            lastGridPosition = gridPos;
        }
        else
        {
            //get grid object from the last position
            if (tempGridObject == null && gridObject.IsPlaced) return null;

            //show placeholder under mouse position
            LevelGrid.Instance.ShowObjectPlaceholder(gridPosition, blockRandomizer.GetSelectedBlock());
            attackPlaceholderPos = ShowAttackPlaceholder(attackGridPosition);

            if (lastGridPosition != gridPosition)
            {
                tempObject.GetComponent<GridObjectVisual>().HideBlockSprite();
                attackPlaceholderPos = HideAttackPlaceholder(lastGridAttackPosition);
                lastGridPosition = gridPosition;
            }
        }
        return attackPlaceholderPos;
    }
    void PlaceBlock(Vector2Int gridPosition, List<Vector2Int> attackGridPosition)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (LevelGrid.Instance.SetGridObjectVisual(blockRandomizer.GetSelectedBlock(), gridPosition))
            {
                HideAttackPlaceholder(lastAttackPlaceholder);
                #region AttackPhase
                int i = 0;
                bool attack = false;

                while (attackGridPosition.Count > i)
                {
                    if (LevelGrid.Instance.GetGridObject(attackGridPosition[i]).block.blockType != Block.BlockType.Default)
                    {
                        attack = true;
                        break;
                    }
                    i++;
                }


                if (attack)
                {
                    ShowAttackPlaceholder(attackGridPosition);
                    gameOver = true;
                }

                #endregion

                //Lebih dari 3
                List<GridObject> gridObjects = LevelGrid.Instance.GetGridObjectSimilar(blockRandomizer.GetSelectedBlock());
                if (gridObjects.Count >= 3 && !gameOver)
                {
                    foreach (GridObject blockObject in gridObjects)
                    {
                        blockObject.UpdateBlock(defaultBlock);
                        blockObject.ResetBlock();
                    }
                }

                score += blockRandomizer.GetSelectedBlock().score;
                blockRandomizer.RandomizeBlock();
                timer = placeTime;
            }


            OnBlockClicked?.Invoke(this, new OnBlockClickedEventArgs
            {
                score = this.score
            });
        }
    }

    private List<Vector2Int> ShowAttackPlaceholder(List<Vector2Int> attackGridPosition)
    {
        for (int x = 0; x < attackGridPosition.Count; x++)
        {
            GridObject attackGridObject = LevelGrid.Instance.GetGridObject(attackGridPosition[x]);
            if (attackGridObject != null)
            {
                Transform attackObject = attackGridObject.gridObject;
                GridObjectVisual attackVisual = attackObject.GetComponent<GridObjectVisual>();
                attackVisual.ShowAttackSprite();
            }
        }

        return attackGridPosition;
    }

    List<Vector2Int> HideAttackPlaceholder(List<Vector2Int> attackGridPosition)
    {
        for (int x = 0; x < attackGridPosition.Count; x++)
        {
            GridObject attackGridObject = LevelGrid.Instance.GetGridObject(attackGridPosition[x]);
            if (attackGridObject != null)
            {
                Transform attackObject = attackGridObject.gridObject;
                GridObjectVisual attackVisual = attackObject.GetComponent<GridObjectVisual>();
                attackVisual.HideAttackSprite();
            }
        }

        return attackGridPosition;
    }

    public void ResetBlockAndScore()
    {
        score = 0;

        foreach(Transform child in transform)
        {
            child.GetComponent<Block>().DeleteThisObject();
        }
        timer = placeTime;
        gameOver = false;

        OnBlockClicked?.Invoke(this, new OnBlockClickedEventArgs
        {
            score = this.score
        });
    }

    public bool GetGameOver()
    {
        return gameOver;
    }

    public Block GetDefaultBlock()
    {
        return defaultBlock;
    }

    public float GetTimerFraction()
    {
        return timer/placeTime;
    }
}
