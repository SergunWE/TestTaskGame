using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class MapSetter : MonoBehaviour
{
    [SerializeField] private UnityEvent<MapComponents> LevelLoaded;

    private MapComponents _currentMap;

    public void LoadMap(LevelDataSo levelDataSo)
    {
        ClearCurrentLevel();
        CreateMap(levelDataSo.LevelComponents);
        LevelLoaded.Invoke(_currentMap);
    }

    private void ClearCurrentLevel()
    {
        if(_currentMap != null)
        {
            Destroy(_currentMap.gameObject);
            _currentMap = null;
        }
    }

    private void CreateMap(MapComponents mapComponents)
    {
        _currentMap = Instantiate(mapComponents);
        _currentMap.MapTilemap.CompressBounds();
    }
}
