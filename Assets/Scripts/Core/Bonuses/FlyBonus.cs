using UnityEngine;
using Zenject;

/*
 * Fly bonus MonoBehaviour. Adding FlyModificator to the Hero on collide.
 */
public class FlyBonus : BaseBonus
{
    [Inject] protected ModificatorsManager _modificatorsManager;

    protected override void OnHeroCollide()
    {
        var mods = _modificatorsManager.Get<IFlyModificator>();

        Debug.Assert(mods.Count == 0);

        _modificatorsManager.AddModificator(FlyModificator.KEY);
    }
}
