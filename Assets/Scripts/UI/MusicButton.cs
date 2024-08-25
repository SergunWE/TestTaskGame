using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MusicButton : MonoBehaviour
{
    [SerializeField] private GameContext gameContext;
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private string onText = "MUSIC ON";
    [SerializeField] private string offText = "MUSIC OFF";

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClick);
        gameContext.MusicChanged += OnMusicChanged;
    }

    private void OnEnable()
    {
        OnMusicChanged();
    }

    private void OnMusicChanged()
    {
        buttonText.text = gameContext.Music ? onText : offText;
    }

    private void OnButtonClick()
    {
        gameContext.Music = !gameContext.Music;
    }
}
