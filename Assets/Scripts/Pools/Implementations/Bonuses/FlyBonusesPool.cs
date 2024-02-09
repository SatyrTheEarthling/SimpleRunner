/*
 * Pool for fly bonus game objects. Implements AddressablePool and IBonusesPool. Provide Key - addressable path to prefab.
 */

public class FlyBonusesPool : AddressablePool<FlyBonus>, IBonusesPool
{
    protected override string Key => "Prefabs/BonusFly.prefab";
}

