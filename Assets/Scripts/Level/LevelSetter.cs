using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class LevelSetter : MonoBehaviour
{
    [SerializeField] private GameContext gameContext;
    [SerializeField] private MapCenter mapCenter;
    [SerializeField] private UnityEvent<MapComponents> LevelLoaded;

    private MapComponents _currentMap;

    public void LoadLevel(LevelDataSo levelDataSo)
    {
        ClearCurrentLevel();
        CreateMap(levelDataSo.LevelComponents);
        CreateMapCenter();
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

    private void CreateMapCenter()
    {
        mapCenter.transform.DetachChildren();
        Vector3 tilemapCenter = _currentMap.MapTilemap.cellBounds.center;
        mapCenter.transform.position = tilemapCenter;
        _currentMap.transform.SetParent(mapCenter.transform, true);
    }
}
