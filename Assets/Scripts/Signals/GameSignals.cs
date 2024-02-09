using Zenject;

/*
 * Signals for game loop
 */
public class GameSignals
{
    public static void Declare(DiContainer container)
    {
        container.DeclareSignal<GameStarted>();
        container.DeclareSignal<RaceEnded>();
    }

    public class GameStarted { }
    public class RaceEnded { }
}
