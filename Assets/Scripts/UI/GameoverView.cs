using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameoverView : MonoBehaviour
{
    [SerializeField] private GameContext gameContext;
    [SerializeField] private Button nextButton;
    [SerializeField, Space] private TMP_Text yourTimeText;
    [SerializeField] private TMP_Text bestTimeText;

    public void ShowView(bool levelPassed)
    {
        nextButton.interactable = levelPassed;
        bestTimeText.text = TimeSpan.FromSeconds(gameContext.CurrentLevel.BestRemainingTimeSeconds).ToString(Constans.TimeFormat);
        gameObject.SetActive(true);
    }

    public void OnTimerStoped(float seconds)
    {
        yourTimeText.text = TimeSpan.FromSeconds(seconds).ToString(Constans.TimeFormat);
    }


}
