using UnityEngine;
using Zenject;

/*
 * Base class for time limited modificators. Implements ITickableModificator, so every frame checks for timeout and remove itself in needed.
 */
public abstract class TimeLimitedModificator : ITickableModificator
{
    abstract public string Key { get; }

    [Inject] protected ModificatorsManager _modificatorsManager;

    public float FinishTime { get; private set; }

    abstract public void OnAdd();
    abstract public void OnRemove();

    public void Prolongate()
    {
        FinishTime = Time.time + 7;
    }

    public void CheckTimeout()
    {
        if (Time.time > FinishTime)
        {
            _modificatorsManager.RemoveModificator(Key);
        }
    }

    public void OnUpdate()
    {
        CheckTimeout();
    }    
}
