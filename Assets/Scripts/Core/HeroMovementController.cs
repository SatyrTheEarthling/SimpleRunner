using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;
using Zenject;

/*
 * Like UnityEngine.Transform but for this game. 
 * This class support positions in restricted area - x: {-1, 1}; y: {0, 2}. Where y = 1 means hero is in jump, and y = 2 - hero is flying.
 * Class support only one axis movements from player. But in case of flying, hero can combine horizontal and vertical movements.
 * 
 * Workflow for movement:
 *  - Update _position.
 *  - Start movement tween.
 *  - Await for tween finish.
 *  - Fire signal FinishMovement.
 *  
 *  Movement timeouts taken from MovementSettings divided on Speed multiplier from ISpeedModificator. So SlowDown Jump will have the same length, like normal ones.
 */
public class HeroMovementController : MonoBehaviour
{
    [Inject] private SignalBus _signalBus;
    [Inject] private CoreSettings _coreSettings;
    [Inject] private MovementSettings _movementSettings;
    [Inject] private ModificatorsManager _modificatorsManager;

    private Vector2Int _position = Vector2Int.zero;
    private Tween _jumpTween = null;

    void Start()
    {
        _signalBus.Subscribe<HeroSignals.NewCommand>(OnNewCommandHandler);
        _signalBus.Subscribe<HeroSignals.FlyBonusTaken>(OnFlyStartHandler);
        _signalBus.Subscribe<HeroSignals.FlyEnd>(OnFlyEndHandler);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<HeroSignals.NewCommand>(OnNewCommandHandler);
    }

    private void OnNewCommandHandler(HeroSignals.NewCommand signal)
    {
        ProcessCommand(signal.Movement);

    }

    private void OnFlyEndHandler()
    {
        StopFlying().Forget();
    }

    private void OnFlyStartHandler()
    {
        StartFlying().Forget();
    }

    /// <summary>
    /// Process movement 
    /// </summary>
    /// <param name="movement"></param>
    private void ProcessCommand(Vector2Int movement)
    {
        if (movement == Vector2Int.zero)
        {
            _signalBus.Fire<HeroSignals.FinishMovement>();
            return;
        }

        // The Hero jumping and he is on the ground.
        if (movement.y == 1 && _position.y == 0)
        {
            Jump().Forget();
            return;
        }

        // The hero not in the middle and movement in the same direction - no movement
        if (_position.x != 0 &&
            Math.Sign(_position.x) == Math.Sign(movement.x))
        {
            _signalBus.Fire<HeroSignals.FinishMovement>();
            return;
        }

        var newPosition = new Vector2Int(Math.Clamp(_position.x + movement.x, -1, 1), _position.y);
        MoveSideTo(newPosition).Forget();
    }

    private async UniTaskVoid MoveSideTo(Vector2Int newPosition)
    {
        var delta = newPosition - _position;
        _position = newPosition;

        float timeout;
        AnimationCurve ease;

        if (Mathf.Abs(delta.x) == 1)
        {
            timeout = _movementSettings.OneRowMovementTimeout / GetSpeedMultiplicator();
            ease = _movementSettings.OneRowEase;
        }
        else
        {
            timeout = _movementSettings.TwoRowsMovementTimeout / GetSpeedMultiplicator();
            ease = _movementSettings.TwoRowsEase;
        }

        var movement = transform
            .DOLocalMoveX(_position.x * _coreSettings.HalfWidthOfTrack, timeout)
            .SetEase(ease);

        await movement.AsyncWaitForKill();

        _signalBus.Fire<HeroSignals.FinishMovement>();
    }

    private async UniTaskVoid Jump()
    {
        _position = new Vector2Int(_position.x, 1);

        _jumpTween = transform
            .DOLocalMoveY(_coreSettings.JumpHeight, _movementSettings.JumpTimeout / GetSpeedMultiplicator())
            .SetEase(_movementSettings.JumpEase);

        await _jumpTween.AsyncWaitForKill();

        _signalBus.Fire<HeroSignals.FinishMovement>();

        // For case, when Jump will be "finished" when Hero is flying.
        if (_position.y == 1)
            _position = new Vector2Int(_position.x, 0);
        _jumpTween = null;
    }


    private async UniTaskVoid StartFlying()
    {
        _position = new Vector2Int(_position.x, 2);

        if (_jumpTween != null)
            _jumpTween.Kill();

        var movement = transform
            .DOLocalMoveY(_coreSettings.FlyHeight, _movementSettings.FlyUpTimeout / GetSpeedMultiplicator())
            .SetEase(_movementSettings.FlyUpEase);

        await movement.AsyncWaitForKill();
    }

    private async UniTaskVoid StopFlying()
    {
        var movement = transform
            .DOLocalMoveY(0, _movementSettings.FlyDownTimeout / GetSpeedMultiplicator())
            .SetEase(_movementSettings.FlyDownEase);

        await movement.AsyncWaitForKill();

        _position = new Vector2Int(_position.x, 0);

    }

    private float GetSpeedMultiplicator()
    {
        var mods = _modificatorsManager.Get<ISpeedModificator>();

        Debug.Assert(mods.Count < 2);

        var multiplicator = 1f;

        if (mods.Count > 0)
            multiplicator += mods[0].GetAdditionalPersent();

        return multiplicator;
    }
}
