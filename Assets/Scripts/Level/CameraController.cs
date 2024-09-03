using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System;

public class CameraController : GameController
{
    [SerializeField] private Camera gameCamera;
    [SerializeField] private float sizeOffset = 0;
    [SerializeField, Space] private float rotationDuration = 1f;
    [SerializeField] private float offsetRotationSpeedMultiplier = 0.5f;
    [SerializeField] private float maxRotationSpeedMultiplier = 2;
    public static event Action OnRotationEnd;
    private Dictionary<MapDirection, float> _directions;
    private float _currentSpeedRotatingMultiplier;
    private bool _currentClockwise;
    private bool _rotatingRunning;

    private void Awake()
    {
        _directions = new()
        {
           {(MapDirection)0, 0},
           {(MapDirection)1, 90},
           {(MapDirection)2, 180},
           {(MapDirection)3, 270},
        };
    }

    public override void Init(MapComponents mapComponents)
    {
        StopAllCoroutines();
        gameCamera.transform.parent.transform.eulerAngles = new Vector3(0, 0, _directions.FirstOrDefault().Value);
        _currentSpeedRotatingMultiplier = 0;
        SetCameraCenter(mapComponents.MapTilemap);
        SetCameraSize(mapComponents.MapTilemap);
    }

    public void RotateCamera(MapDirection gravityDirection, bool clockwise)
    {
        StopAllCoroutines();
        if (_currentClockwise != clockwise || !_rotatingRunning)
        {
            _currentSpeedRotatingMultiplier = 0;
        }
        _currentSpeedRotatingMultiplier = Math.Clamp(_currentSpeedRotatingMultiplier + offsetRotationSpeedMultiplier, 1, maxRotationSpeedMultiplier);

        _currentClockwise = clockwise;
        StartCoroutine(RotateCoroutine(gravityDirection, clockwise));
    }

    private void SetCameraCenter(Tilemap tilemap)
    {
        Vector3 tilemapCenter = tilemap.cellBounds.center;
        gameCamera.transform.parent.position = new Vector3(tilemapCenter.x, tilemapCenter.y, gameCamera.transform.parent.position.z);
    }

    private void SetCameraSize(Tilemap tilemap)
    {
        Bounds tilemapBounds = tilemap.localBounds;
        float aspectRatio = Screen.width / (float)Screen.height;
        float verticalSize = Mathf.Max(tilemapBounds.size.y / 2f, tilemapBounds.size.x / 2f);
        float horizontalSize = Mathf.Max(tilemapBounds.size.x / 2f / aspectRatio, tilemapBounds.size.y / 2f / aspectRatio);
        gameCamera.orthographicSize = Mathf.Max(verticalSize, horizontalSize) + sizeOffset;
    }

    private IEnumerator RotateCoroutine(MapDirection direction, bool clockwise)
    {
        _rotatingRunning = true;
        var currentAngle = gameCamera.transform.parent.transform.eulerAngles.z % 360;
        var targetAngle = _directions[direction];
        //принудительный поворот по часовой/против, если меньшая дуга противоположная
        var angleDiff = currentAngle - targetAngle;
        switch (clockwise)
        {
            case true:
                if (angleDiff > 0)
                {
                    targetAngle += 360;
                }
                break;
            case false:
                if (angleDiff < 0)
                {
                    targetAngle -= 360;
                }
                break;
        }
        //Debug.Log($"Current: {currentAngle} Target {targetAngle}");

        float elapsedTime = 0f;
        float duration = rotationDuration / _currentSpeedRotatingMultiplier;
        //Debug.Log(duration);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAngle = Mathf.Lerp(currentAngle, targetAngle, elapsedTime / duration);
            //Debug.Log(newAngle);
            gameCamera.transform.parent.transform.eulerAngles = new Vector3(0, 0, newAngle);
            yield return null;
        }

        gameCamera.transform.parent.transform.eulerAngles = new Vector3(0, 0, _directions[direction]);
        _rotatingRunning = false;
        _currentSpeedRotatingMultiplier = 0;
        OnRotationEnd?.Invoke();
    }
}
