using Cysharp.Threading.Tasks;
using System;
using Zenject;

/*
 * SimpleStoryTeller - simple implementation of IStoryTeller. 
 * StoryTeller is responsible for creating obstacles at the right moments. 
 * This one - generate random obstacles with some delay. Difficulty increased with time, delay - decreased.
 */
public class SimpleStoryTeller: IStoryTeller
{
    [Inject] private BonusesSettings _bonusesSettings;
    [Inject] private IRandomGenerator _random;
    private bool _isInterrupted = false;

    private Action<ObstaclePattern> _obstacleCreatedCallback;


    public void Run(Action<ObstaclePattern> callback)
    {
        _obstacleCreatedCallback = callback;
        StartStoryLoop().Forget();
    }

    public void Stop()
    {
        _isInterrupted = true;
    }

    /// <summary>
    /// Starts race loop and work until StoryTeller will be interrupted.
    /// </summary>
    private async UniTaskVoid StartStoryLoop()
    {
        var difficulty = 0.3f;
        var delay = 1500;

        while (true)
        {
            GenerateObstacle(difficulty);
            difficulty += 0.007f;
            if (delay > 800)
                delay -= 5;
            await UniTask.Delay(delay);

            if (_isInterrupted)
                break;
        }
    }

    /// <summary>
    /// Generate obstacle with provided difficulty.
    /// Difficulty if just a float value. 
    /// Every time this generator adding nw block to obstacle, it subtract random 0..1 value from difficulty. 
    /// So result quantity of obstacle blocks is random.
    /// When obstacle pattern is defined, adds random bonus to the any free point in obstacle.
    /// </summary>
    private void GenerateObstacle(float difficulty)
    {
        var obstaclePattern = new ObstaclePattern();
        var holesLeft = 6;
        do
        {
            var rand = _random.Range(0,2);
            if (rand == 0)
            {
                obstaclePattern.AddLeftObstacle();
            }
            else if (rand == 1)
            {
                obstaclePattern.AddCenterObstacle();
            }
            else
            {
                obstaclePattern.AddRightObstacle();
            }

            difficulty -= _random.Value;
            holesLeft--;

        } while (difficulty > 0 && holesLeft > 1);

        var freePositions = obstaclePattern.GetFreePositions();
        
        var pos = 0;

        if (freePositions.Length > 1)
            pos = freePositions[_random.Range(0, freePositions.Length - 1)];
        else
            pos = freePositions[0];

        obstaclePattern.AddBonus(GetNewBonusType(), pos);

        _obstacleCreatedCallback(obstaclePattern);
    }

    private Type GetNewBonusType()
    {
        var types = new Type[] {
            typeof(CoinBonus),
            typeof(SpeedUpBonus),
            typeof(SlowDownBonus),
            typeof(FlyBonus)
        };

        var weights = new float[] {
            _bonusesSettings.CoinChanceWeight,
            _bonusesSettings.SpeedUpChanceWeight,
            _bonusesSettings.SlowDownChanceWeight,
            _bonusesSettings.FlyChanceWeight
        };

        var bonusNumber = _random.ChooseOne(weights);

        return types[bonusNumber];
    }
}
