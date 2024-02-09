using Zenject;

/*
 * Declares in-game Signals.
 */
public class SignalsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        PlayerInputSignals.Declare(Container);
        HeroSignals.Declare(Container);
        GameSignals.Declare(Container);

        Container.DeclareSignal<CoinBonus.CoinTakenSignal>();
    }
}