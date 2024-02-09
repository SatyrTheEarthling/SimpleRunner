using System.Collections.Generic;
using UnityEngine;
using Zenject;

/*
 * ILevelController implementation. 
 * Responsible for:
 *  - Adding of obstacles by ObstaclePattern.
 *  - Clearing of level.
 *  - Moving obstacles every frame with speed based on CoreSettings and ISpeedModificator.
 *  
 *  Takes obstacles from ObstaclesPool and push them back, when they are behind of the Hero.
 */
public class LevelController : MonoBehaviour, ILevelController
{
    [Inject] private CoreSettings _coreSettings;
    [Inject] private ObstaclesPool _obstaclesPool;
    [Inject] private ModificatorsManager _modificatorsManager;

    [SerializeField] private Transform _startPoint;

    private List<ObstacleController> _obstacles = new List<ObstacleController>();

    public void AddObstacle(ObstaclePattern pattern)
    {
        var obstacle = _obstaclesPool.Pop();
        obstacle.transform.SetParent(transform);
        obstacle.transform.position = _startPoint.position;
        _obstacles.Add(obstacle);

        obstacle.Setup(pattern);
    }

    public void ClearLevel()
    {
        while (_obstacles.Count > 0)
        {
            HideObstacle(_obstacles[0]);
        }
    }

    private void FixedUpdate()
    {
        int i = 0;
        var speed = GetSpeed();

        while (i < _obstacles.Count)
        {
            var obstacle = _obstacles[i];
            MoveObstacle(obstacle, speed);

            if (IsObstaclePassed(obstacle))
            {
                HideObstacle(obstacle);
            } else
            {
                i++;
            }
        }
    }

    private void HideObstacle(ObstacleController obstacle)
    {
        _obstacles.Remove(obstacle);
        _obstaclesPool.Push(obstacle);
    }

    private bool IsObstaclePassed(ObstacleController obstacle)
    {
        return obstacle.transform.position.z < -0.5f;
    }

    private void MoveObstacle(ObstacleController obstacle, float speed)
    {
        obstacle.MoveOn(speed * Time.fixedDeltaTime);
    }

    private float GetSpeed()
    {
        var mods = _modificatorsManager.Get<ISpeedModificator>();

        Debug.Assert(mods.Count < 2);

        var multiplicator = 1f;

        if (mods.Count > 0)
            multiplicator += mods[0].GetAdditionalPersent();

        return _coreSettings.Speed * multiplicator;
    }
}
