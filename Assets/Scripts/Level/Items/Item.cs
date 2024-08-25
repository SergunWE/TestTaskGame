using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    public event Action<Item> ItemActivated;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //тэг легко проверить, но мне не нравится поиск по строкам
        //можно было сделать поиск по компоненту игрока (если такой имеется)
        if (other.CompareTag(Constans.PlayerTag))
        {
            ActivateItem();
        }
    }

    protected virtual void ActivateItem()
    {
        ItemActivated?.Invoke(this);
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        ItemActivated = null;
    }
}