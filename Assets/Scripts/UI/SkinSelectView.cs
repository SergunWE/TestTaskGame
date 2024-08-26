using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSelectView : MonoBehaviour
{
    [SerializeField] private GameContext gameContext;
    [SerializeField] private Transform skinButtonsRoot;
    [SerializeField] private SkinButton skinButtonPrefab;

    private List<SkinButton> skinButtons= new List<SkinButton>();

    private void OnEnable()
    {
        foreach (Transform child in skinButtonsRoot)
        {
            Destroy(child.gameObject);
        }
        skinButtons.Clear();

        for (int i = 0; i < gameContext.Skins.Count; i++)
        {
            var skinButton = Instantiate(skinButtonPrefab, skinButtonsRoot);
            skinButtons.Add(skinButton);
            skinButton.Init(gameContext.Skins[i], gameContext.CurrentSkin == gameContext.Skins[i]);
            skinButton.SkinClicked += OnSkinClicked;
        }
    }

    private void OnSkinClicked(PlayerSkinSo playerSkin)
    {
        if(playerSkin.Purchased)
        {
            gameContext.CurrentSkin = playerSkin;
        }
        else
        {
            if (gameContext.CurrentCurrency >= playerSkin.Cost)
            {
                gameContext.CurrentCurrency -= playerSkin.Cost;
                playerSkin.Purchased = true;
            }
        }

        RefreshSkins();
    }

    private void RefreshSkins()
    {
        foreach(var skinButton in skinButtons)
        {
            skinButton.Refresh(gameContext.CurrentSkin);
        }
    }
}
