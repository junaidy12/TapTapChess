using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Transform gameOver;
    [SerializeField] Image imageTimer;

    private void Start()
    {
        scoreText.text = "SCORE : 0";

        BlockPlacer.Instance.OnBlockClicked += BlockPlacer_OnBlockClicked;
    }
    private void Update()
    {
        if (BlockPlacer.Instance.GetGameOver())
        {
            gameOver.gameObject.SetActive(true);
        }
        else
        {
            gameOver.gameObject.SetActive(false);
        }

        imageTimer.fillAmount = BlockPlacer.Instance.GetTimerFraction();
    }

    private void BlockPlacer_OnBlockClicked(object sender, BlockPlacer.OnBlockClickedEventArgs e)
    {
        scoreText.text = $"SCORE : " + e.score.ToString();
    }


}
