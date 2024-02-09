using TMPro;
using UnityEngine;
using Zenject;

/*
 * UI component that listen for CoinBonus.CoinTakenSignal and GameSignals.RaceEnded signals and update text with quantity.
 */
public class CoinQuanityComponent : MonoBehaviour
{
    [Inject] private SignalBus _signalBus;

    [SerializeField] private TextMeshProUGUI _coinsQuantityText;

    private int _quanity = 0;

    void Start()
    {
        _signalBus.Subscribe<CoinBonus.CoinTakenSignal>(OnCoinTakenHandler);
        _signalBus.Subscribe<GameSignals.RaceEnded>(OnRaceEndedHandler);
    }

    private void OnRaceEndedHandler()
    {
        _quanity = 0;
        UpdateQuanityText();
    }

    private void UpdateQuanityText()
    {
        _coinsQuantityText.text = _quanity.ToString();
    }

    private void OnCoinTakenHandler()
    {
        _quanity++;
        _coinsQuantityText.text = _quanity.ToString();
    }
}

