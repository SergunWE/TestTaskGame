using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MapCenter : MonoBehaviour
{
    public Rigidbody2D Rigidbody => _rd ?? GetComponent<Rigidbody2D>();

    private Rigidbody2D _rd;

    private void Awake()
    {
        _rd = GetComponent<Rigidbody2D>();
    }
}
