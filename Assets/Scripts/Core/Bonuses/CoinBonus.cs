/*
 * Coin bonus MonoBehaviour. Call CoinTakenSignal on collide with Hero.
 */
public class CoinBonus : BaseBonus
{
    protected override void OnHeroCollide()
    {
        _signalBus.Fire<CoinTakenSignal>();
    }

    public class CoinTakenSignal { }
}
