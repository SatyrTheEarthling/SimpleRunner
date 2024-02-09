using Zenject;
using UnityEngine;

/*
 * Signals related to Player's Input
 */
public class PlayerInputSignals
{
    public static void Declare(DiContainer container)
    {
        container.DeclareSignal<NewAction>();
    }

    public class NewAction
    {
        public readonly Vector2Int Movement;

        public NewAction(Vector2Int movement)
        {
            Movement = movement;
        }
    }
}

