using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// макароны
/// </summary>
public class MenuSetter : MonoBehaviour
{
    [SerializeField] private List<Canvas> canvas;
    [SerializeField] private UnityEvent menuCanvasRequested;
    [SerializeField] private UnityEvent gameCanvasRequested;
    [SerializeField, Space] private List<GameObject> menus;
    [SerializeField] private UnityEvent mainMenuRequested;
    [SerializeField] private UnityEvent levelMenuRequested;
    [SerializeField] private UnityEvent settingsMenuRequested;
    [SerializeField] private UnityEvent bonusMenuRequested;
    [SerializeField] private UnityEvent shopMenuRequested;

    private void Awake()
    {
        ShowMainMenu();
    }

    public void Policy()
    {
        Application.OpenURL("https://flup.space/head?t=p");
    }
    
    public void Contact()
    {
        Application.OpenURL("https://flup.space/head?t=c");
    }
    
    public void ShowGame()
    {
        ClearCanvas();
        gameCanvasRequested?.Invoke();
    }

    public void ShowMainMenu()
    {
        ClearMenus();
        mainMenuRequested?.Invoke();
    }

    public void ShowLevelMenu()
    {
        ClearMenus();
        levelMenuRequested?.Invoke();
    }

    public void ShowSettingsMenu()
    {
        ClearMenus();
        settingsMenuRequested?.Invoke();
    }

    public void ShowShopMenu()
    {
        ClearMenus();
        shopMenuRequested?.Invoke();
    }

    private void ClearCanvas()
    {
        foreach (var canvas in canvas)
        {
            canvas.gameObject.SetActive(false);
        }
    }

    private void ClearMenus()
    {
        foreach (var menu in menus)
        {
            menu.SetActive(false);
        }
        ClearCanvas();
        menuCanvasRequested?.Invoke();
    }
}
