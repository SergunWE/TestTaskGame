using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameContext", menuName = "SOData/GameContext")]
public class GameContext : ScriptableObject
{
    [field:SerializeField] public LevelDataSo CurrentLevel { get; private set; }
    [field:SerializeField] public List<LevelDataSo> Levels { get; private set; }
}
