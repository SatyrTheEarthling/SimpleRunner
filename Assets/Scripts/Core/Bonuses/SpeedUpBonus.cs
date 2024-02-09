using System;

/*
 * Speed up bonus MonoBehaviour. Implements BaseSpeedBonus and provide (implemnent) ModificatorType and ModificatorKey for it logic.
 */
public class SpeedUpBonus : BaseSpeedBonus
{
    protected override Type ModificatorType => typeof(SpeedUpModificator);
    protected override string ModificatorKey => SpeedUpModificator.KEY;
}
