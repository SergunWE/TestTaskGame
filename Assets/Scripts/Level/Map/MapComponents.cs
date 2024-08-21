using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapComponents : MonoBehaviour
{
    [field: SerializeField] public Tilemap MapTilemap { get; private set; }
    [field: SerializeField] public Transform StartPlayerPosition { get; private set; }
    [field: SerializeField] public EndLevel EndLevel { get; private set; }
    [field: SerializeField] public List<Jewelry> Justifies{ get; private set; }

    private void OnValidate() {
        if(transform != null)
        {
            if(transform.parent != null)
            {
                Debug.LogWarning("Warning! The map component container must be the parent object", this);
            }
        }
    }
}