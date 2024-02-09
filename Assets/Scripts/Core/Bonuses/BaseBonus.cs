using UnityEngine;
using Zenject;

/*
 * Base class for in-game bonuses.
 * Functions:
 *  - Looking for OnTriggerEnter with Hero.
 *  - Rotating bonus model.
 */
public abstract class BaseBonus : MonoBehaviour, IPoolable
{
    [Inject] protected SignalBus _signalBus;
    [SerializeField] private Transform _rotationAxis;

    abstract protected void OnHeroCollide();

    public void Reset()
    {
    }

    void Update()
    {
        _rotationAxis.Rotate(Vector3.up, 180f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnHeroCollide();
    }
}
