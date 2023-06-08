using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObjectVisual : MonoBehaviour
{
    [SerializeField] Transform objectSprite;

    GridObject gridObject;
    SpriteRenderer spriteRenderer;
    public void SetupGridVisual(GridObject gridObject)
    {
        this.gridObject = gridObject;
        spriteRenderer = objectSprite.GetComponent<SpriteRenderer>();
        Block block = BlockPlacer.Instance.GetDefaultBlock();
        gridObject.UpdateBlock(block, gridObject.IsClicked);

        BlockPlacer.Instance.OnBlockClicked += BlockPlacer_OnBlockClicked;
    }
    private void BlockPlacer_OnBlockClicked(object sender, EventArgs e)
    {
        spriteRenderer.sprite = gridObject.block.sprite;
    }

}
