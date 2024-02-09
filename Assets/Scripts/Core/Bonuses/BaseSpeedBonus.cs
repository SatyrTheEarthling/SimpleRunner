using System;
using UnityEngine;
using Zenject;

/*
 * Base bonus class for speed bonuses. 
 * Game designer (in my head), promise me, that only one implementation of ISpeedModificator can effect on Hero.
 * Base class knows his ModificatorType and ModificatorKey from abstract properies. 
 * So class provide logic called when Hero meet this bonus. It may be:
 *  - Add new modificator.
 *  - Prolongate exists modificator, if it has the same Key.
 *  - Remove exists modificator, and add new.
 */
public abstract class BaseSpeedBonus : BaseBonus
{
    [Inject] protected ModificatorsManager _modificatorsManager;

    abstract protected Type ModificatorType { get; }
    abstract protected string ModificatorKey { get; }

    protected override void OnHeroCollide()
    {
        var mods = _modificatorsManager.Get<ISpeedModificator>();

        Debug.Assert(mods.Count < 2);

        if (mods.Count == 0)
        {
            _modificatorsManager.AddModificator(ModificatorKey);
            return;
        }

        if (mods[0].GetType() == ModificatorType)
        {
            mods[0].Prolongate();
            return;
        }
        else
        {
            _modificatorsManager.RemoveModificator(mods[0].Key);
        }

        _modificatorsManager.AddModificator(ModificatorKey);
    }
}
