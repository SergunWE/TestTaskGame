using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    [SerializeField] private float gravityForce = 10;
    private Dictionary<MapDirection, Vector2> _directions;

    private void Awake()
    {
        _directions = new()
        {
           {(MapDirection)0, new(0,-gravityForce)},
           {(MapDirection)1, new(gravityForce,0)},
           {(MapDirection)2, new(0,gravityForce)},
           {(MapDirection)3, new(-gravityForce,0)},
        };
    }

    public void Reset()
    {
        SetDefaultGravity();
    }

    public void RotateGravity(MapDirection gravityDirection, bool clockwise)
    {
        SetGravity(_directions[gravityDirection]);
    }

    private void SetDefaultGravity()
    {
        SetGravity(_directions.FirstOrDefault().Value);
    }

    private void SetGravity(Vector2 vector)
    {
        Physics2D.gravity = vector;
    }
}
