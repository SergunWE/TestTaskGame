using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera gameCamera;
    [SerializeField] private Vector3 centerOffset = Vector3.zero;
    [SerializeField] private float sizeOffset = 0;

    private Tilemap _levelTilemap;

    public void Prepare(Tilemap tilemap)
    {
        _levelTilemap = tilemap;
        SetCameraCenter();
        SetCameraSize();
    }

    private void SetCameraCenter()
    {
        Vector3 tilemapCenter = _levelTilemap.cellBounds.center;
        gameCamera.transform.position = new Vector3(tilemapCenter.x, tilemapCenter.y, gameCamera.transform.position.z) + centerOffset;
    }

    private void SetCameraSize()
    {
        Bounds tilemapBounds = _levelTilemap.localBounds;
        float aspectRatio = Screen.width / (float)Screen.height;
        float verticalSize = Mathf.Max(tilemapBounds.size.y / 2f, tilemapBounds.size.x / 2f);
        float horizontalSize = Mathf.Max(tilemapBounds.size.x / 2f / aspectRatio, tilemapBounds.size.y / 2f / aspectRatio);
        gameCamera.orthographicSize = Mathf.Max(verticalSize, horizontalSize) + sizeOffset;
    }
}
