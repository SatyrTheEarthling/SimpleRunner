/*
 * Pool for slow down bonus game objects. Implements AddressablePool and IBonusesPool. Provide Key - addressable path to prefab.
 */

public class SpeedUpBonusesPool : AddressablePool<SpeedUpBonus>, IBonusesPool
{
    protected override string Key => "Prefabs/BonusSpeedUp.prefab";
}

