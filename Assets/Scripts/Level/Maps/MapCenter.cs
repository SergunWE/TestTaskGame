using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MapCenter : MonoBehaviour
{
    public Rigidbody2D Rigidbody => _rd != null ? _rd : GetComponent<Rigidbody2D>();

    private Rigidbody2D _rd;

    public void Init(MapComponents mapComponents)
    {
        transform.DetachChildren();
        Vector3 tilemapCenter = mapComponents.MapTilemap.cellBounds.center;
        transform.position = tilemapCenter;
        mapComponents.transform.SetParent(transform, true);
    }

    private void Awake()
    {
        _rd = GetComponent<Rigidbody2D>();
    }
}
