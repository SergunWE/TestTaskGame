using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    [SerializeField] private float gravityForce = 10;
    private List<Vector2> _directions;
    private int _directionCount;
    private int _currentIndex;

    private void Awake()
    {
        _directions = new List<Vector2>()
        {
            new(0,-gravityForce),
            new(gravityForce,0),
            new(0,gravityForce),
            new(-gravityForce,0),
        };
        _directionCount = _directions.Count;

        SetDefaultGravity();
    }

    public void RotateGravity(bool clockwise)
    {
        _currentIndex = clockwise ? _currentIndex + 1 : _currentIndex - 1;
        SetGravity(_directions[GetCorrectIndex()]);
    }

    private void SetDefaultGravity()
    {
        SetGravity(_directions.FirstOrDefault());
    }

    private void SetGravity(Vector2 vector)
    {
        Physics2D.gravity = vector;
    }

    private int GetCorrectIndex()
    {
        if (_currentIndex >= 0)
        {
            return _currentIndex % _directionCount;
        }
        else
        {
            return _directionCount - Math.Abs(_currentIndex % _directionCount);
        }
    }
}
