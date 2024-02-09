/*
 * Pool for slow down bonus game objects. Implements AddressablePool and IBonusesPool. Provide Key - addressable path to prefab.
 */

public class SlowDownBonusesPool : AddressablePool<SlowDownBonus>, IBonusesPool
{
    protected override string Key => "Prefabs/BonusSlowDown.prefab";
}

