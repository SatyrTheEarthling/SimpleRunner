/*
 * Pool for obstacle game objects. Implements PrewarmedAddressablePool and IBonusesPool. Provide Key - addressable path to prefab.
 */
public class ObstaclesPool : PrewarmedAddressablePool<ObstacleController>
{
    protected override string Key => "Prefabs/Obstacle.prefab";
}
