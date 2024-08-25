using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Управляет монетками и бонусом времени
/// </summary>
public class ItemController : GameController
{
    [SerializeField] private GameContext gameContext;
    [SerializeField] private UnityEvent<TimeBonus> timeBonusCollected;

    private int _collectedCurrency;
    private LevelDataSo _levelData;

    public override void Init(MapComponents mapComponents)
    {
        _collectedCurrency = 0;
        _levelData = gameContext.CurrentLevel;
        foreach (var item in mapComponents.Items)
        {
            item.ItemActivated += OnItemActivated;
        }
    }

    public void OnLevelPassed()
    {
        GiveAward();
    }

    private void OnItemActivated(Item item)
    {
        switch (item)
        {
            case Coin coin:
                Debug.Log("Coin");
                OnCoinActivated(coin);
                break;
            case TimeBonus timeBonus:
                Debug.Log("TimeBonus");
                OnTimeBonusActiveted(timeBonus);
                break;
        }
    }

    private void OnCoinActivated(Coin coin)
    {
        _collectedCurrency += coin.Award;
    }

    private void OnTimeBonusActiveted(TimeBonus timeBonus)
    {
        timeBonusCollected?.Invoke(timeBonus);
    }

    private void GiveAward()
    {
        var availableReward = Math.Clamp(_levelData.MaxCurrency - _levelData.CollectedCurrency, 0, _levelData.MaxCurrency);
        var currentCurrency = Math.Min(availableReward, _collectedCurrency);
        gameContext.CurrentCurrency += currentCurrency;
        _levelData.CollectedCurrency += currentCurrency;
    }
}
