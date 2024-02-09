using Zenject;

/*
 * FlyModificator implements IFlyModificator and call FlyBonusTaken on add, and FlyEnd on timeout.
 */
public class FlyModificator : TimeLimitedModificator, IFlyModificator
{
    public const string KEY = "FlyModificator";

    [Inject] private SignalBus _signalBus;

    public override string Key => KEY;

    public override void OnAdd()
    {
        Prolongate();
        _signalBus.Fire(new HeroSignals.FlyBonusTaken());
    }

    public override void OnRemove()
    {
        _signalBus.Fire(new HeroSignals.FlyEnd());
    }
}
