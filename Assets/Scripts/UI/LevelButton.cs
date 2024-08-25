using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private TMP_Text levelNumberText;
    [SerializeField] private Button button;

    public event Action<int> LevelSelected;

    private int _levelNumber;

    private void Awake()
    {
        button.onClick.AddListener(() => LevelSelected?.Invoke(_levelNumber));
    }

    public void Init(int levelNumber, bool avalible)
    {
        _levelNumber = levelNumber;
        levelNumberText.text = levelNumber.ToString();
        button.interactable = avalible;
    }

    private void OnDestroy()
    {
        LevelSelected = null;
    }
}
