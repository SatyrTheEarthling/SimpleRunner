/*
 * Pool for coin bonus game objects. Implements AddressablePool and IBonusesPool. Provide Key - addressable path to prefab.
 */
public class CoinBonusesPool : AddressablePool<CoinBonus>, IBonusesPool
{
    protected override string Key => "Prefabs/BonusCoin.prefab";
}

