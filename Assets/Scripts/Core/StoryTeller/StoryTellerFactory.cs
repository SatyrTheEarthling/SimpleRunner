using Zenject;

public interface IStoryTellerFactory
{
    IStoryTeller Create();
}

/*
 * Simple Factory that creates IStoryTellers. 
 */
public class StoryTellerFactory<T> : IStoryTellerFactory where T : IStoryTeller
{
    readonly DiContainer _container;

    public StoryTellerFactory(DiContainer container)
    {
        _container = container;
    }

    public IStoryTeller Create()
    {
        return _container.Instantiate<T>();
    }
}
