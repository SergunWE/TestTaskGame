using UnityEngine;
using UnityEngine.Events;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private GameContext gameContext;
    [SerializeField] private UnityEvent<LevelDataSo> currentLevelSet;

    private void Start()
    {
        //debug
        LoadLevel();
    }

    private void LoadLevel()
    {
        currentLevelSet?.Invoke(gameContext.CurrentLevel);
    }
}