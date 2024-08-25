using UnityEngine;

public class Coin : Item
{
    [field: SerializeField] public int Award { get; private set; } = 1;

    protected override void ActivateItem()
    {
        base.ActivateItem();
    }
}