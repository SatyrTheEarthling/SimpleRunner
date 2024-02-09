using System;

/*
 * Slow down bonus MonoBehaviour. Implements BaseSpeedBonus and provide (implemnent) ModificatorType and ModificatorKey for it logic.
 */
public class SlowDownBonus : BaseSpeedBonus
{
    protected override Type ModificatorType => typeof(SlowDownModificator);
    protected override string ModificatorKey => SlowDownModificator.KEY;
}
