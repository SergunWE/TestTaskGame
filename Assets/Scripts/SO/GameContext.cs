using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameContext", menuName = "SOData/GameContext")]
public class GameContext : ScriptableObject
{
    public event Action CurrencyChanged;

    [field: SerializeField] public LevelDataSo CurrentLevel { get; set; }
    [SerializeField] private int currentCurrency;
    [field: SerializeField] public List<LevelDataSo> Levels { get; private set; }

    public int CurrentCurrency
    {
        get => currentCurrency;
        set
        {
            currentCurrency = value;
            CurrencyChanged?.Invoke();
        }
    }
}
