using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "SOData/LevelsData")]
public class LevelDataSo : LevelDataBaseSo
{
    [field: SerializeField] public float TimeSeconds { get; private set; } = 30f;
    [field: SerializeField] public MapComponents LevelComponents { get; private set; }

    public float BestTimeSeconds;
}
