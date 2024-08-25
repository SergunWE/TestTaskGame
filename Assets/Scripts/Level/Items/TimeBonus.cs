using System;
using TMPro;
using UnityEngine;

public class TimeBonus : Item
{
    [field: SerializeField] public float TimeSeconds { get; private set; } = 5;
    [SerializeField] private TMP_Text timeText;

    private void Awake()
    {
        timeText.text = TimeSpan.FromSeconds(TimeSeconds).TotalSeconds.ToString();
    }

    protected override void ActivateItem()
    {
        base.ActivateItem();
    }
}