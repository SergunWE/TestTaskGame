using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "GameContext", menuName = "SOData/GameContext")]
[Serializable]
public class GameContext : ScriptableObject
{
    public event Action CurrencyChanged;
    public event Action MusicChanged;

    [field: SerializeField] public LevelDataSo CurrentLevel { get; set; }
    [SerializeField] private int currentCurrency;
    [field: SerializeField] public List<LevelDataSo> Levels { get; private set; }

    [SerializeField, Space] private bool musicOn = true;

    [field: SerializeField, Space] public PlayerSkinSo CurrentSkin { get; set; }
    [field: SerializeField] public List<PlayerSkinSo> Skins { get; private set; }

    private const string SaveKey = "GameContext";

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
        get => musicOn;
        set
        {
            musicOn = value;
            MusicChanged?.Invoke();
        }
    }

    public void SetNextLevel()
    {
        CurrentLevel.IsCompleted = true;
        int currentLevelIndex = Levels.IndexOf(CurrentLevel);
        if (currentLevelIndex >= 0)
        {
            CurrentLevel = Levels[Math.Clamp(currentLevelIndex + 1, 0, Levels.Count - 1)];
        }
    }

    [ContextMenu("Clear")]
    private void Clear()
    {
        CurrentLevel = Levels.FirstOrDefault();
        currentCurrency = 0;
        foreach (var level in Levels)
        {
            level.Clear();
        }
        musicOn = true;
        CurrentSkin = Skins.FirstOrDefault();
        foreach (var skin in Skins)
        {
            skin.Clear();
        }
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }

    private void OnDestroy()
    {
        CurrencyChanged = null;
        MusicChanged = null;
    }

    #region Storage
    public void Save()
    {
        var saveData = new GameContextSaveData
        {
            CurrentCurrency = currentCurrency,
            MusicOn = musicOn,
            CurrentLevelIndex = Levels.IndexOf(CurrentLevel),
            CurrentSkinIndex = Skins.IndexOf(CurrentSkin),
            Levels = Levels.Select(level => new LevelDataSaveData
            {
                IsCompleted = level.IsCompleted,
                CollectedCurrency = level.CollectedCurrency,
                BestRemainingTimeSeconds = level.BestRemainingTimeSeconds
            }).ToList(),
            Skins = Skins.Select(skin => new PlayerSkinSaveData
            {
                Purchased = skin.Purchased
            }).ToList()
        };

        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (!PlayerPrefs.HasKey(SaveKey))
        {
            Clear();
            return;
        }

        string json = PlayerPrefs.GetString(SaveKey);
        var saveData = JsonUtility.FromJson<GameContextSaveData>(json);

        currentCurrency = saveData.CurrentCurrency;
        musicOn = saveData.MusicOn;
        CurrentLevel = Levels.ElementAtOrDefault(saveData.CurrentLevelIndex);
        CurrentSkin = Skins.ElementAtOrDefault(saveData.CurrentSkinIndex);

        for (int i = 0; i < saveData.Levels.Count; i++)
        {
            Levels[i].IsCompleted = saveData.Levels[i].IsCompleted;
            Levels[i].CollectedCurrency = saveData.Levels[i].CollectedCurrency;
            Levels[i].BestRemainingTimeSeconds = saveData.Levels[i].BestRemainingTimeSeconds;
        }

        for (int i = 0; i < saveData.Skins.Count; i++)
        {
            Skins[i].Purchased = saveData.Skins[i].Purchased;
        }
    }
    #endregion
}
