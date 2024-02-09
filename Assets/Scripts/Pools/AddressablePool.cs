using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

/*
 * AddressablePool extends BasePool and implement abstract method Instantiate. Pool instantiate objects from addressable gained by abstract propery Key.
 */
public abstract class AddressablePool<T> : BasePool<T> where T : MonoBehaviour, IPoolable
{
    [Inject] DiContainer _diContainer;
    
    protected abstract string Key { get; }

    private GameObject _prefab;

    protected override T Instantiate()
    {
        if (_prefab != null)
        {
            return Instantiate(_prefab);
        }

        var opHandler = Addressables.LoadAssetAsync<GameObject>(Key);
        _prefab = opHandler.WaitForCompletion();

        return Instantiate(_prefab);
    }

    private T Instantiate(GameObject asset)
    {
        var result = Object.Instantiate(asset).GetComponent<T>();
        _diContainer.Inject(result);

        return result;
    }

    public override void Push(T obj)
    {
        base.Push(obj);
    }
}
