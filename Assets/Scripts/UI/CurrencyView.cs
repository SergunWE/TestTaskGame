using TMPro;
using UnityEngine;

public class CurrencyView : MonoBehaviour
{
    [SerializeField] private GameContext gameContext;
    [SerializeField] private TMP_Text text;

    private void OnEnable()
    {
        gameContext.CurrencyChanged += CurrencyChanged;
        UpdateView();
    }

    private void OnDisable()
    {
        gameContext.CurrencyChanged -= CurrencyChanged;
    }

    private void CurrencyChanged()
    {
        UpdateView();
    }

    public void UpdateView()
    {
        text.text = gameContext.CurrentCurrency.ToString();
    }
}
