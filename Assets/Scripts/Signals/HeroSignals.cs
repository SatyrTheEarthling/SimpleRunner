using UnityEngine;
using Zenject;

/*
 * Signals related to the Hero.
 */
public class HeroSignals
{
    public static void Declare(DiContainer container)
    { 
        container.DeclareSignal<HitObstacle>();
        container.DeclareSignal<HeroDead>();
        container.DeclareSignal<NewCommand>();
        container.DeclareSignal<FinishMovement>();
        container.DeclareSignal<FlyBonusTaken>();
        container.DeclareSignal<FlyEnd>();
    }

    public class HitObstacle { }
    public class HeroDead { }

    public class NewCommand {
        public readonly Vector2Int Movement;

        public NewCommand(Vector2Int movement)
        {
            Movement = movement;
        }
    }

    public class FinishMovement { }

    public class FlyBonusTaken { }
    public class FlyEnd { }
}
