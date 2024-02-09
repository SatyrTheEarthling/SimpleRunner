/*
 * SlowDown Modificator implements BaseSpeedModificator and provide, Speed Multiplier and modificator Key.
 */
public class SlowDownModificator : BaseSpeedModificator
{
    public const string KEY = "SlowDownModificator";
    public override string Key => KEY;

    public override float GetAdditionalPersent()
    {
        return -0.33f;
    }
}
