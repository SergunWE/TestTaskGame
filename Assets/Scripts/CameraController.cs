using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Camera gameCamera;

    private void Awake()
    {
        tilemap.CompressBounds();
        SetCameraCenter();
        SetCameraSize();
    }

    void SetCameraCenter()
    {
        Vector3 tilemapCenter = tilemap.cellBounds.center;
        gameCamera.transform.position = new Vector3(tilemapCenter.x, tilemapCenter.y, gameCamera.transform.position.z);
    }

    private void SetCameraSize()
    {
        Bounds tilemapBounds = tilemap.localBounds;
        float aspectRatio = Screen.width / (float)Screen.height;
        float verticalSize = tilemapBounds.size.y / 2f;
        float horizontalSize = tilemapBounds.size.x / 2f / aspectRatio;
        gameCamera.orthographicSize = Mathf.Max(verticalSize, horizontalSize);
    }
}
