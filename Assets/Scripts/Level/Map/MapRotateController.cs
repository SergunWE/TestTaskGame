using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MapRotateController : MonoBehaviour
{
    [SerializeField] private UnityEvent<MapDirection, bool> mapRotated;

    private int _enumCount;
    private byte _currentDirection;

    private void Awake()
    {
        _enumCount = Enum.GetNames(typeof(MapDirection)).Length;
    }

    public void RotateClockwise()
    {
        _currentDirection++;
        Rotate((MapDirection)(_currentDirection % _enumCount), true);
    }

    public void RotateConterclockwise()
    {
        _currentDirection--;
        Rotate((MapDirection)(_currentDirection % _enumCount), false);
    }

    private void Rotate(MapDirection direction, bool clockwise)
    {
        StopAllCoroutines();
        mapRotated?.Invoke(direction, clockwise);
        //Debug.Log($"{clockwise} {direction}");
    }
}