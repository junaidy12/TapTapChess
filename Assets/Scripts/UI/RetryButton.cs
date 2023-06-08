using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetryButton : MonoBehaviour
{
    [SerializeField] Button retryButton;

    private void Start()
    {
        retryButton.onClick.AddListener(() =>
        {
            LevelGrid.Instance.ResetLevel();
        });
    }
}
