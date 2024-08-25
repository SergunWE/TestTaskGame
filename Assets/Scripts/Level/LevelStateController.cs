using UnityEngine;
using UnityEngine.Events;

public class LevelStateController : GameController
{
    [SerializeField] private GameContext gameContext;
    [SerializeField] private UnityEvent levelPaused;
    [SerializeField] private UnityEvent levelResume;
    [SerializeField] private UnityEvent levelPassed;
    [SerializeField] private UnityEvent levelFailed;
    [SerializeField] private UnityEvent levelExit;

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

    public void OnGameExit()
    {
        levelExit?.Invoke();
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
        gameContext.CurrentLevel.IsCompleted = true;
        _gameOver = true;
        levelPassed?.Invoke();
    }
}
