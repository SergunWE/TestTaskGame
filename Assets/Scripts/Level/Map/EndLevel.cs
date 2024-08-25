using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    public event Action LevelPassed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //тэг легко проверить, но мне не нравится поиск по строкам
        //можно было сделать поиск по компоненту игрока (если такой имеется)
        if (other.CompareTag(Constans.PlayerTag))
        {
            LevelPassed?.Invoke();
        }
    }

    private void OnDestroy()
    {
        LevelPassed = null;
    }
}
