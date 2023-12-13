using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObjectVisual : MonoBehaviour
{
    [SerializeField] Transform bgSprite;
    [SerializeField] Transform blockSprite;
    [SerializeField] Transform attackSprite;

    [SerializeField] GridObject gridObject;
    SpriteRenderer spriteRenderer;
    public void SetupGridVisual(GridObject gridObject)
    {
        this.gridObject = gridObject;
        spriteRenderer = bgSprite.GetComponent<SpriteRenderer>();
        Block block = BlockPlacer.Instance.GetDefaultBlock();
        gridObject.UpdateBlock(block);
        gridObject.ResetBlock();

        BlockPlacer.Instance.OnBlockClicked += BlockPlacer_OnBlockClicked;
    }
    private void BlockPlacer_OnBlockClicked(object sender, EventArgs e)
    {
        spriteRenderer.sprite = gridObject.block.sprite;
    }

    public void SetBlockSprite(Sprite sprite)
    {
        blockSprite.gameObject.SetActive(true);
        blockSprite.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void HideBlockSprite()
    {
        blockSprite.gameObject.SetActive(false);
    }

    public void ShowAttackSprite()
    {
        attackSprite.gameObject.SetActive(true);
    }
    public void HideAttackSprite()
    {
        attackSprite.gameObject.SetActive(false);
    }
}
