using UnityEngine;
using Zenject;

/*
 * PlayerInputController for editor
 */
public class KeyboardPlayerInputController : MonoBehaviour
{
    [Inject] private SignalBus _signalBus;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
            _signalBus.Fire(new PlayerInputSignals.NewAction(Vector2Int.left));
        else if (Input.GetKeyDown(KeyCode.Keypad3))
            _signalBus.Fire(new PlayerInputSignals.NewAction(Vector2Int.right));
        else if (Input.GetKeyDown(KeyCode.Keypad4))
            _signalBus.Fire(new PlayerInputSignals.NewAction(Vector2Int.left * 2));
        else if (Input.GetKeyDown(KeyCode.Keypad5))
            _signalBus.Fire(new PlayerInputSignals.NewAction(Vector2Int.up));
        else if (Input.GetKeyDown(KeyCode.Keypad6))
            _signalBus.Fire(new PlayerInputSignals.NewAction(Vector2Int.right * 2));
        else if (Input.GetKeyDown(KeyCode.Keypad8))
            _signalBus.Fire(new HeroSignals.FlyBonusTaken());
        else if (Input.GetKeyDown(KeyCode.Keypad0))
            _signalBus.Fire(new HeroSignals.FlyEnd());
    }
}

