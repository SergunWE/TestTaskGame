using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TimerController : GameController
{
    [SerializeField] private GameContext gameContext;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private UnityEvent<float> timeStopedSeconds;
    [SerializeField] private UnityEvent timeUp;

    private Stopwatch _stopwatch;
    private TimeSpan _levelTime;

    public override void Init(MapComponents mapComponents)
    {
        StopAllCoroutines();
        _levelTime = TimeSpan.FromSeconds(gameContext.CurrentLevel.TimeSeconds);
        _stopwatch = Stopwatch.StartNew();
        StartCoroutine(TimerCoroutine());
    }

    public void OnTimeBonusCollected(TimeBonus timeBonus)
    {
        AddTimeSeconds(timeBonus.TimeSeconds);
    }

    public void AddTimeSeconds(float seconds)
    {
        _levelTime += TimeSpan.FromSeconds(seconds);
    }

    public void OnLevelPassed()
    {
        _stopwatch.Stop();
        var remainingTimeSeconds = gameContext.CurrentLevel.TimeSeconds - _stopwatch.Elapsed.TotalSeconds;
        gameContext.CurrentLevel.BestRemainingTimeSeconds = Mathf.Max((float)remainingTimeSeconds, gameContext.CurrentLevel.BestRemainingTimeSeconds);
        timeStopedSeconds?.Invoke((float)remainingTimeSeconds);
    }

    private IEnumerator TimerCoroutine()
    {
        TimeSpan timeSpan = _levelTime - _stopwatch.Elapsed;
        while (timeSpan > TimeSpan.Zero)
        {
            timerText.text = timeSpan.ToString(Constans.TimeFormat);
            yield return null;
            timeSpan = _levelTime - _stopwatch.Elapsed;
        }
        timeUp?.Invoke();
    }
}
