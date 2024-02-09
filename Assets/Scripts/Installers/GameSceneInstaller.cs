using UnityEngine;
using Zenject;

/*
 * Zenject.MonoInstaller of game scene. Prewarm ObstaclesPool, and makes other things that should or may be called on this scene
 */

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] private LevelController _levelController;

    public override void InstallBindings()
    {
        Container.Resolve<ObstaclesPool>().Prewarm(30);
        Container.Bind<ILevelController>().FromInstance(_levelController);

        Container.Bind<GameLoopController>().AsSingle().NonLazy();
    }
}
