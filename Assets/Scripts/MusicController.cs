using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
    [SerializeField] private GameContext gameContext;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        gameContext.MusicChanged += OnMusicChanged;
        OnMusicChanged();
    }

    private void OnMusicChanged()
    {
        _audioSource.mute = !gameContext.Music;
    }
}
