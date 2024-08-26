using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private GameContext gameContext;
    [SerializeField] private UnityEvent levelStartLoading;
    [SerializeField] private UnityEvent<LevelDataSo> currentLevelSet;

    private void Start()
    {
        //debug
        //LoadLevel();
    }

    public void LoadLevel()
    {
        levelStartLoading?.Invoke();
        currentLevelSet?.Invoke(gameContext.CurrentLevel);
    }

    public void LoadMaxAvalibleLevel()
    {
        int levelIndex = gameContext.Levels.FindLastIndex(x => x.IsCompleted == true) + 1;
        LoadLevelByIndex(levelIndex);
    }

    public void LoadNextLevel()
    {
        int levelIndex = gameContext.Levels.IndexOf(gameContext.CurrentLevel) + 1;
        LoadLevelByIndex(levelIndex);
    }

    private void LoadLevelByIndex(int index)
    {
        gameContext.CurrentLevel = gameContext.Levels[Math.Clamp(index, 0, gameContext.Levels.Count - 1)];
        LoadLevel();
    }
}