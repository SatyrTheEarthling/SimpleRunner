/*
 * SpeedUp Modificator implements BaseSpeedModificator and provide, Speed Multiplier and modificator Key.
 */
public class SpeedUpModificator : BaseSpeedModificator
{
    public const string KEY = "SpeedUpModificator";
    public override string Key => KEY;

    public override float GetAdditionalPersent()
    {
        return 0.5f;
    }
}

