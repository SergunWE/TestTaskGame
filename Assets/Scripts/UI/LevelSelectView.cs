using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelSelectView : MonoBehaviour
{
    [SerializeField] private GameContext gameContext;
    [SerializeField] private Transform levelButtonsRoot;
    [SerializeField] private LevelButton levelButtonPrefab;
    [SerializeField] private UnityEvent<LevelDataSo> levelSelected;

    private void OnEnable()
    {
        foreach (Transform child in levelButtonsRoot)
        {
            Destroy(child.gameObject);
        }

        bool prevLevelCompleted = true;
        for (int i = 0; i < gameContext.Levels.Count; i++)
        {
            var levelButton = Instantiate(levelButtonPrefab, levelButtonsRoot);
            levelButton.Init(i + 1, prevLevelCompleted || gameContext.Levels[i].IsCompleted);
            levelButton.LevelSelected += OnLevelSelected;
            prevLevelCompleted = gameContext.Levels[i].IsCompleted;
        }
    }

    private void OnLevelSelected(int number)
    {
        levelSelected?.Invoke(gameContext.Levels[number - 1]);
    }
}
