using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    public event Action ItemActivated;

    private void OnTriggerEnter2D(Collider2D other)
    {
        ActivateItem();
    }

    protected virtual void ActivateItem()
    {
        ItemActivated?.Invoke();
        gameObject.SetActive(false);
    }
}