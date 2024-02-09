using Zenject;
using UnityEngine;

/*
 * Factory for PlayerInputControllers. Provide possibility to define right PlayerInputController in Installer, and instantitate in any other place, when needed.
 */
public interface IPlayerInputControllerFactory
{
    MonoBehaviour Create(GameObject targetGameObject);
}

public class PlayerInputControllerFactory<T> : IPlayerInputControllerFactory where T: MonoBehaviour
{
    readonly DiContainer _container;

    public PlayerInputControllerFactory(DiContainer container)
    {
        _container = container;
    }

    public MonoBehaviour Create(GameObject targetGameObject)
    {
        return _container.InstantiateComponent<T>(targetGameObject);
    }
}
