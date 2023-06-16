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
            PlaceBlock()();
        }
    }
    void PlaceBlock()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2Int gridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetMousePosition());
            if (LevelGrid.Instance.SetGridObjectVisual(blockRandomizer.GetSelectedBlock(), gridPosition, false))
            {

                #region AttackPhase

                List<Vector2Int> invalidPositionList = blockRandomizer.GetSelectedBlock().CheckUnvalidGridPosition(
                    gridPosition, LevelGrid.Instance.GetGridSize());

                int i = 0;
                bool attack = false;

                while (invalidPositionList.Count > i)
                {
                    if (LevelGrid.Instance.GetGridObject(invalidPositionList[i]).block.blockType != Block.BlockType.Default)
                    {
                        attack = true;
                        break;
                    }
                    i++;
                }


                for (int x = 0; x < invalidPositionList.Count; x++)
                {
                    if (attack)
                    {
                        Block atkBlock = Instantiate(attackBlock,
                            new Vector3(LevelGrid.Instance.GetWorldPosition(invalidPositionList[x]).x,
                            LevelGrid.Instance.GetWorldPosition(invalidPositionList[x]).y,
                            -.03f),
                            Quaternion.identity, transform);

                        if (LevelGrid.Instance.GetGridObject(invalidPositionList[x]).block.blockType != Block.BlockType.Default)
                        {

                            SpriteRenderer spriteRenderer = atkBlock.transform.GetComponentInChildren<SpriteRenderer>();
                            spriteRenderer.color = new Color(1, 1, 1, 0.7f);

                            gameOver = true;

                        }
                    }
                }

                #endregion

                //Lebih dari 3
                List<GridObject> gridObjects = LevelGrid.Instance.GetGridObjectSimilar(blockRandomizer.GetSelectedBlock());
                if (gridObjects.Count >= 3 && !gameOver)
                {
                    foreach (GridObject blockObject in gridObjects)
                    {
                        blockObject.UpdateBlock(defaultBlock, false);
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
