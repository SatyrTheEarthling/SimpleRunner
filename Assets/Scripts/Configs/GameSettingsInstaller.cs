using UnityEngine;
using Zenject;

/*
 * Zenject ScriptableObjectInstaller for game settings. Bind instances of multiple Settings classes.
 */
[CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    public CoreSettings CoreSettings;
    public MovementSettings MovementSettings;
    public BonusesSettings BonusesSettings;
    public SwipeInputSettings SwipeInputSettings;

    public override void InstallBindings()
    {
        Container.BindInstances(CoreSettings);
        Container.BindInstances(MovementSettings);
        Container.BindInstances(BonusesSettings);
        Container.BindInstances(SwipeInputSettings);
    }
}
