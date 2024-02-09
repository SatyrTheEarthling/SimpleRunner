using UnityEngine;
using Zenject;

/*
 * ObstacleController.
 * Obstacle is 6 cubes, 3 cubes wide and 2 cubes up. 
 * Controller has access to cubes by _parts array. They should be "stored" in the order: left to right, down to up.
 * Uses provided ObstaclePattern to hide some cubes and put bonus on place of one of them.
 * Takes bonuses from BonusesPool and push them back on Reset (when it backs to the pool itself)
 */
[SelectionBase]
public class ObstacleController : MonoBehaviour, IPoolable
{
    [Inject] private SignalBus _signalBus;
    [Inject] private BonusesPool _bonusesPool;

    [Tooltip("Left to right, downt to up")]
    [SerializeField] private GameObject[] _parts;
    [SerializeField] private Rigidbody _rigidbody;


    private BaseBonus _bonus = null;

    public void Setup(ObstaclePattern pattern)
    {
        for (int i = 0; i < _parts.Length; i++)
        {
            _parts[i].SetActive(pattern.IsHasObstacle(i));
        }

        _bonus = _bonusesPool.Pop(pattern.Bonus) as BaseBonus;
        _bonus.transform.SetParent(transform);
        _bonus.transform.position = _parts[pattern.BonusPosition].transform.position;
    }

    public void MoveOn(float delta)
    {
        var pos = _rigidbody.position;
        pos.z -= delta;
        _rigidbody.MovePosition(pos);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _signalBus.Fire<HeroSignals.HitObstacle>();
    }

    public void Reset()
    {
        if (_bonus)
            _bonusesPool.Push(_bonus);
    }
}
