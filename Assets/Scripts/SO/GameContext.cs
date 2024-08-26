using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameContext", menuName = "SOData/GameContext")]
public class GameContext : ScriptableObject
{
    public event Action CurrencyChanged;
    public event Action MusicChanged;

    [field: SerializeField] public LevelDataSo CurrentLevel { get; set; }
    [SerializeField] private int currentCurrency;
    [field: SerializeField] public List<LevelDataSo> Levels { get; private set; }

    [SerializeField, Space] private bool _musicOn = true;

    [field: SerializeField, Space] public PlayerSkinSo CurrentSkin{ get; set; }
    [field: SerializeField] public List<PlayerSkinSo> Skins { get; private set; }

    public int CurrentCurrency
    {
        get => currentCurrency;
        set
        {
            currentCurrency = value;
            CurrencyChanged?.Invoke();
        }
    }

    public bool Music
    {
        get => _musicOn;
        set
        {
            _musicOn = value;
            MusicChanged?.Invoke();
        }
    }

    private void OnDestroy()
    {
        CurrencyChanged = null;
        MusicChanged = null;
    }
}
