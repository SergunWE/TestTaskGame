using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Canvas mainMenuCanvas;
    [SerializeField] private Canvas gameCanvas;

    private void Awake()
    {
        ShowMenuScreen();
    }

    public void ShowMenuScreen()
    {
        mainMenuCanvas.enabled = true;
        gameCanvas.enabled = false;
    }

    public void ShowLoadingScreen()
    {
        mainMenuCanvas.enabled = false;
        gameCanvas.enabled = false;
    }

    public void ShowGameScreen()
    {
        mainMenuCanvas.enabled = false;
        gameCanvas.enabled = true;
    }
}
