using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRandomizer : MonoBehaviour
{
    [SerializeField] Transform nextBlockToPlaceVisual;

    [SerializeField] Block selectedBlock;

    Block[] blockArray;

    private void Awake()
    {
        blockArray = GetComponents<Block>();
    }

    private void Start()
    {
        RandomizeBlock();
    }

    public void RandomizeBlock()
    {
        int range = blockArray.Length;
        int random = UnityEngine.Random.Range(0, range);
        if (blockArray[random].include)
            selectedBlock = blockArray[random];
        else
            RandomizeBlock();

        nextBlockToPlaceVisual.GetComponent<SpriteRenderer>().sprite = selectedBlock.sprite;
    }

    public Block GetSelectedBlock()
    {
        return selectedBlock;
    }

} 
