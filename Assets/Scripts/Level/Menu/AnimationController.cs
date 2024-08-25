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
        mainMenuCanvas.gameObject.SetActive(true);
        gameCanvas.gameObject.SetActive(false);
    }

    public void ShowLoadingScreen()
    {
        mainMenuCanvas.gameObject.SetActive(false);
        gameCanvas.gameObject.SetActive(false);
    }

    public void ShowGameScreen()
    {
        mainMenuCanvas.gameObject.SetActive(false);
        gameCanvas.gameObject.SetActive(true);
    }
}
