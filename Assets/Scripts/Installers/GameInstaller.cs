using UnityEngine;
using Zenject;

/*
 * Main Zenject.MonoInstaller of the Game
 */
public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        InstallInputControllerFactory();

        Container.BindInterfacesAndSelfTo<ModificatorsManager>().AsSingle();
        Container.BindInterfacesTo<RandomFromUnityGenerator>().AsSingle();
        Container.Instantiate<HeroController>();

        BindBonusesPools();
        Container.Bind<ObstaclesPool>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();

        Container.Bind<IStoryTellerFactory>().To<StoryTellerFactory<SimpleStoryTeller>>().AsSingle();
    }

    private void InstallInputControllerFactory()
    {
        if (Application.isEditor)
        {
            Container.Bind<IPlayerInputControllerFactory>().To<PlayerInputControllerFactory<KeyboardPlayerInputController>>().AsSingle();
        } else
        {
            Container.Bind<IPlayerInputControllerFactory>().To<PlayerInputControllerFactory<SwipePlayerInputController>>().AsSingle();
        }
    }

    private void BindBonusesPools()
    {
        Container.BindInterfacesTo<CoinBonusesPool>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        Container.BindInterfacesTo<SpeedUpBonusesPool>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        Container.BindInterfacesTo<SlowDownBonusesPool>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        Container.BindInterfacesTo<FlyBonusesPool>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();

        Container.Bind<BonusesPool>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
    }
}