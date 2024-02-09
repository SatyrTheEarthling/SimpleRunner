/*
 * Base Modificator for ISpeedModificator. 
 * Extends TimeLimitedModificator - start timer (prolongate) on Add.
 */
public abstract class BaseSpeedModificator : TimeLimitedModificator, ISpeedModificator
{
    public override string Key { get; }

    abstract public float GetAdditionalPersent();

    public override void OnAdd()
    {
        Prolongate();
    }

    public override void OnRemove()
    {
    }
}
