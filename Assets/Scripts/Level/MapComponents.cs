using UnityEngine;
using UnityEngine.Tilemaps;

public class MapComponents : MonoBehaviour
{
    [field:SerializeField] public Grid Grid {get; private set;}
    [field: SerializeField] public Tilemap MapTilemap { get; private set; }
    [field: SerializeField] public Tilemap PropsTilemap { get; private set; }

    private void OnValidate() {
        if(transform != null)
        {
            if(transform.root != null)
            {
                Debug.LogWarning("Warning! The map component container must be the parent object", this);
            }
        }
    }
}