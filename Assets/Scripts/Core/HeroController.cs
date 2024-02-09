using Zenject;

/*
 *  This class processing Hero things. Subscribed on 
 *   - HitObstacle. So race will be restarted on it.
 *   - NewAction from player input. Hero ignors any movement until finished previouse one.
 *   - FinishMovement - so Hero can accept new one.
 */
public class HeroController
{
    private SignalBus _signalBus;
    private bool _isMoving = false;

    public HeroController(SignalBus signalBus)
    {
        _signalBus = signalBus;

        SubscribeOnSignals();
    }

    ~HeroController()
    {
        UnsubscribeFromSignals();
    }

    private void SubscribeOnSignals()
    {
        _signalBus.Subscribe<HeroSignals.HitObstacle>(OnHitObstacleHandler);
        _signalBus.Subscribe<PlayerInputSignals.NewAction>(OnPlayerActionHandler);
        _signalBus.Subscribe<HeroSignals.FinishMovement>(OnFinishMovementHandler);
    }

    private void UnsubscribeFromSignals()
    {
        _signalBus.Subscribe<HeroSignals.HitObstacle>(OnHitObstacleHandler);
        _signalBus.Subscribe<PlayerInputSignals.NewAction>(OnPlayerActionHandler);
    }

    private void OnHitObstacleHandler()
    {
        _signalBus.Fire<GameSignals.RaceEnded>();
    }

    private void OnPlayerActionHandler(PlayerInputSignals.NewAction signal)
    {
        if (_isMoving)
            return;

        _isMoving = true;

        _signalBus.Fire(new HeroSignals.NewCommand(signal.Movement));
    }

    private void OnFinishMovementHandler()
    {
        _isMoving = false;
    }
}
