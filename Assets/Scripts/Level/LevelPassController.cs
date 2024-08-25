using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelPassController : MonoBehaviour
{
    [SerializeField] private UnityEvent levelPassed;
    [SerializeField] private UnityEvent levelFailed;
}
