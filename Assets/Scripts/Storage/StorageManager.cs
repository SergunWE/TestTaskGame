using UnityEngine;

public class StorageManager : MonoBehaviour
{
    [SerializeField] private GameContext gameContext;

#if !UNITY_EDITOR
    private void Awake()
    {
        gameContext.Load();
    }

    private void OnApplicationQuit()
    {
        gameContext.Save();
    }
#endif
}