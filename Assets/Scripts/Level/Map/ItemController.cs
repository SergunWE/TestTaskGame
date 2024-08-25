using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Управляет камушками и бонусом времени
/// </summary>
public class ItemController : MonoBehaviour
{
    [SerializeField] private GameContext gameContext;

    public void Init(MapComponents mapComponents)
    {
        foreach (var item in mapComponents.Items)
        {
            item.ItemActivated += OnItemActivated;
        }
    }

    private void OnItemActivated(Item item)
    {
        switch (item)
        {
            case Coin coin:
                Debug.Log("Coin");
                OnCoinActivated(coin);
                break;
            case TimeBonus:
                Debug.Log("TimeBonus");
                break;
        }
    }

    private void OnCoinActivated(Coin coin)
    {
        gameContext.CurrentCurrency += coin.Award;
    }
}
