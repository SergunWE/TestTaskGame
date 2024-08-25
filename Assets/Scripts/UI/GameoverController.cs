using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameoverController : GameController
{
    [SerializeField] private GameObject gameoverView;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button levelsButton;
    [SerializeField, Space] private TMP_Text yourTimeText;
    [SerializeField] private TMP_Text bestTimeText;

    [SerializeField, Space] private UnityEvent retryRequested;
    [SerializeField] private UnityEvent nextRequested;
    [SerializeField] private UnityEvent levelsRequested;

    private void Awake()
    {
        retryButton.onClick.AddListener(() => retryRequested.Invoke());
        nextButton.onClick.AddListener(() => nextRequested.Invoke());
        levelsButton.onClick.AddListener(() => levelsRequested.Invoke());
    }

    public override void Init(MapComponents mapComponents)
    {
        gameoverView.SetActive(false);
    }

    public void ShowView(bool pass)
    {
        nextButton.interactable = pass;
        UpdateTimes();
        gameoverView.SetActive(true);
    }

    private void UpdateTimes()
    {

    }


}
