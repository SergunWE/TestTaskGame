using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class MapSetter : MonoBehaviour
{
    [SerializeField] private Tilemap debug;
    [SerializeField] private UnityEvent<Tilemap> MapReady;

    private void Awake()
    {
        //load map from SO
        var map = debug;
        map.CompressBounds();
        MapReady.Invoke(map);
    }
}
