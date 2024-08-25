using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelPassController : GameController
{
    [SerializeField] private UnityEvent levelPassed;
    [SerializeField] private UnityEvent levelFailed;

    private bool _gameOver;

    public override void Init(MapComponents mapComponents)
    {
        _gameOver = false;
        mapComponents.EndLevel.LevelPassed += OnLevelPassed;
    }

    public void OnTimeUp()
    {
        OnLevelFailed();
    }

    private void OnLevelFailed()
    {
        if (_gameOver) return;
        Debug.Log("OnLevelFailed");
        _gameOver = true;
        levelFailed?.Invoke();
    }

    private void OnLevelPassed()
    {
        if(_gameOver) return;
        Debug.Log("LevelPassed");
        _gameOver = true;
        levelPassed?.Invoke();
    }
}
