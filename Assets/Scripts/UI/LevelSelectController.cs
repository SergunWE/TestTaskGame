using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelSelectController : MonoBehaviour
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

        for (int i = 0; i < gameContext.Levels.Count; i++)
        {
            var levelButton = Instantiate(levelButtonPrefab, levelButtonsRoot);
            levelButton.Init(i + 1, i == 0 || gameContext.Levels[i].IsCompleted);
            levelButton.LevelSelected += OnLevelSelected;
        }
    }

    private void OnLevelSelected(int number)
    {
        levelSelected?.Invoke(gameContext.Levels[number - 1]);
    }
}
