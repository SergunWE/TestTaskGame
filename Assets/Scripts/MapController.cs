using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    [SerializeField] private MapCenter _mapCenter;
    [SerializeField] private float rotationSpeed = 90.0f;

    private Tilemap _levelTilemap;
    private Transform _tilemapParent;

    private float targetAngle;
    private float angularSpeed;
    private bool isRotating = false;
    private float startAngle;
    private float elapsedTime;
    private Rigidbody2D rb => _mapCenter.Rigidbody;

    public void Prepare(Tilemap tilemap)
    {
        _levelTilemap = tilemap;
        _tilemapParent = tilemap.gameObject.transform.root;
        CreateMapCenter();
    }

    public void ChangeAngle(float offset)
    {
        targetAngle += offset;
    }

    private void CreateMapCenter()
    {
        _mapCenter.transform.DetachChildren();
        Vector3 tilemapCenter = _levelTilemap.cellBounds.center;
        _mapCenter.transform.position = tilemapCenter;
        _tilemapParent.SetParent(_mapCenter.transform, true);
    }

    private void FixedUpdate() {
        float step = rotationSpeed * Time.fixedDeltaTime; // Calculate step size based on speed
        float newAngle;
        newAngle = Mathf.MoveTowardsAngle(rb.rotation, targetAngle, step);
        rb.MoveRotation(newAngle);
    }
}
