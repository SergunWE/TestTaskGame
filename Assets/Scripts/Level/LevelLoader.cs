using System;
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

    public void LoadCurrentLevel()
    {
        levelStartLoading?.Invoke();
        currentLevelSet?.Invoke(gameContext.CurrentLevel);
    }

    public void LoadNextLevel()
    {
        int currentLevelIndex = gameContext.Levels.IndexOf(gameContext.CurrentLevel);
        if(currentLevelIndex >= 0)
        {
            gameContext.CurrentLevel = gameContext.Levels[Math.Clamp(currentLevelIndex + 1, 0, gameContext.Levels.Count - 1)];
        }
        LoadCurrentLevel();
    }
}