using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MapRotateController : MonoBehaviour
{
    [SerializeField] private UnityEvent<MapDirection, bool> mapRotated;
    [SerializeField] private FaceTracking _faceTracking;
    private int _enumCount;
    private byte _currentDirection;
    private bool _isAbleForRotation;
    
    private void Awake()
    {
        _enumCount = Enum.GetNames(typeof(MapDirection)).Length;
    }

    private void Start()
    {
        _isAbleForRotation = true;
        CameraController.OnRotationEnd += () => _isAbleForRotation = true;
    }

    private void Update()
    {
        if (_isAbleForRotation)
        {
            if (_faceTracking.HeadRotation > 15)
            {
                RotateClockwise();
                _isAbleForRotation = false;
            }
            else if (_faceTracking.HeadRotation < -15)
            {
                RotateConterclockwise();
                _isAbleForRotation = false;
            }
        }
        
    }


    
    public void Reset()
    {
        _currentDirection = 0;
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