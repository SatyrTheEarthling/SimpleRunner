/*
 * General interface for Speed Modificators. 
 */
public interface ISpeedModificator : IModificator
{
    string Key { get; }
    /// <summary>
    /// Returns speed adjutment to calculate speed multiplier.
    /// </summary>
    /// <returns>0..1</returns>
    float GetAdditionalPersent();
    void Prolongate();
    
}

