using System;
using System.Collections.Generic;

[Serializable]
public class GameContextSaveData
{
    public int CurrentCurrency;
    public bool MusicOn;
    public int CurrentLevelIndex;
    public int CurrentSkinIndex;
    public List<LevelDataSaveData> Levels;
    public List<PlayerSkinSaveData> Skins;
}
