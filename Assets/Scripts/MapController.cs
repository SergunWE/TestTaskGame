using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    [SerializeField] private MapCenter _mapCenter;

    private Tilemap _levelTilemap;
    private Transform _tilemapParent;

    public void Prepare(Tilemap tilemap)
    {
        _levelTilemap = tilemap;
        _tilemapParent = tilemap.gameObject.transform.root;
        CreateMapCenter();
    }

    private void CreateMapCenter()
    {
        _mapCenter.transform.DetachChildren();
        Vector3 tilemapCenter = _levelTilemap.cellBounds.center;
        _mapCenter.transform.position = tilemapCenter;
        _tilemapParent.SetParent(_mapCenter.transform, true);
    }
}
