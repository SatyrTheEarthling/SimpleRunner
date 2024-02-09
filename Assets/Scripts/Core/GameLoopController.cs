using Zenject;

/*
 * Class responsible for race loops and restarting of the race. 
 * Using ILevelController to manipulate the level.
 * Subscribed on Signals:
 *  - GameStarted (called from loading screen). Creates new StoryTeller and run it. 
 *  - RaceEnded called from HeroController.  Stops StoryTeller, clear the level and starts new race.
 */
public class GameLoopController
{
    private SignalBus _signalBus;
    private IStoryTellerFactory _storyTellerFactory;
    private ILevelController _levelController;

    private IStoryTeller _storyTeller;

    public GameLoopController(SignalBus signalBus, IStoryTellerFactory storyTellerFactory, ILevelController levelObstaclesController)
    {
        _signalBus = signalBus;
        _storyTellerFactory = storyTellerFactory;
        _levelController = levelObstaclesController;

        _signalBus.Subscribe<GameSignals.GameStarted>(OnGameStartedHandler);
        _signalBus.Subscribe<GameSignals.RaceEnded>(OnRaceEndedHandler);
    }

    private void Run()
    {
        _storyTeller = _storyTellerFactory.Create();
        _storyTeller.Run(OnObstacleCreatedHandler);
    }

    private void OnObstacleCreatedHandler(ObstaclePattern pattern)
    {
        _levelController.AddObstacle(pattern);
    }

    private void OnRaceEndedHandler()
    {
        _storyTeller.Stop();
        _levelController.ClearLevel();
        Run();
    }

    private void OnGameStartedHandler()
    {
        Run();
    }

}
