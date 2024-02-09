using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

/*
 * Simple game UI controller. 
 * Procide functionality:
 *  - Pause button.
 *  - Creates PlayerInputController for _inputArea, using factory.
 */
public class GameUIController : MonoBehaviour
{
    [Inject] private IPlayerInputControllerFactory _playerInputControllerFactory;

    [SerializeField] private Button _pauseButton;

    [SerializeField] private GameObject _inputArea;
    [SerializeField] private TextMeshProUGUI _coinsQuantityText;

    void Start()
    {
        _playerInputControllerFactory.Create(_inputArea);
        _pauseButton.onClick.AddListener(OnPauseClickHandler);
    }

    private void OnPauseClickHandler()
    {
        if (Time.timeScale == 0)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    }
}
