using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "Level", menuName = "SOData/LevelsData")]
public class LevelDataSo : LevelDataBaseSo
{
    [field: SerializeField] public MapComponents LevelComponents { get; private set; }
    [field: SerializeField] public float TimeSeconds { get; private set; } = 31f;
    [field: SerializeField] public float BestRemainingTimeSeconds { get; set; }
    [field: SerializeField] public int MaxCurrency { get; private set; }
    [field: SerializeField] public int CollectedCurrency { get; set; }

    public float BestTimeSeconds => TimeSeconds - BestRemainingTimeSeconds;

    [ContextMenu("Clear")]
    public void Clear()
    {
        IsCompleted = false;
        CollectedCurrency = 0;
        BestRemainingTimeSeconds = 0;
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }
}
