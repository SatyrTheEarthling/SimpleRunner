/*
 * Interface for Modificator that need to process every frame.
 */
public interface ITickableModificator: IModificator
{
    void OnUpdate();
}

