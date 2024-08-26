using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SkinButton : MonoBehaviour
{
    [SerializeField] private GameObject costView;
    [SerializeField] private GameObject useView;
    [SerializeField] private Image skinImage;
    [SerializeField] private TMP_Text skinNameText;
    [SerializeField] private TMP_Text costText;

    public event Action<PlayerSkinSo> SkinClicked;

    private Button _button;
    private PlayerSkinSo _playerSkin;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClick);
    }

    public void Init(PlayerSkinSo playerSkin, bool use)
    {
        _playerSkin = playerSkin;
        skinImage.sprite = playerSkin.MenuSprite;
        skinNameText.text = playerSkin.Name;
        costText.text = playerSkin.Cost.ToString();
        if(playerSkin.Cost <= 0)
        {
            playerSkin.Purchased = true;
        }
        costView.SetActive(!playerSkin.Purchased);
        useView.SetActive(use);
    }

    public void Refresh(PlayerSkinSo currentSkin)
    {
        costView.SetActive(!_playerSkin.Purchased);
        useView.SetActive(currentSkin == _playerSkin);
    }

    private void OnButtonClick()
    {
        SkinClicked?.Invoke(_playerSkin);
    }

    private void OnDestroy()
    {
        SkinClicked = null;
    }
}
