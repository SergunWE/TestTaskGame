using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Camera gameCamera;
    [SerializeField] private Vector3 centerOffset = Vector3.zero;
    [SerializeField] private float sizeOffset = 0;

    private void Awake()
    {
        tilemap.CompressBounds();
        SetCameraCenter();
        SetCameraSize();
    }

    void SetCameraCenter()
    {
        Vector3 tilemapCenter = tilemap.cellBounds.center;
        gameCamera.transform.position = new Vector3(tilemapCenter.x, tilemapCenter.y, gameCamera.transform.position.z) + centerOffset;
    }

    private void SetCameraSize()
    {
        Bounds tilemapBounds = tilemap.localBounds;
        float aspectRatio = Screen.width / (float)Screen.height;
        float verticalSize = Mathf.Max(tilemapBounds.size.y / 2f, tilemapBounds.size.x / 2f);
        float horizontalSize = Mathf.Max(tilemapBounds.size.x / 2f / aspectRatio, tilemapBounds.size.y / 2f / aspectRatio);
        gameCamera.orthographicSize = Mathf.Max(verticalSize, horizontalSize) + sizeOffset;
    }
}
