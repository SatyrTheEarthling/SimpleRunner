using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

/*
 * CombinedPool can unite several pools that implements some T interface.
 * Useful, when you need one pool for different but related objects. 
 * Good example - bonuses. 
 * Provide two Pop methods - generic and with Type as parameter.
 */
public abstract class CombinedPool<T> : MonoBehaviour where T : ITypedPool
{
    protected Dictionary<Type, ITypedPool> _pools = new Dictionary<Type, ITypedPool>();

    [Inject]
    void Construct(DiContainer container)
    {
        var pools = container.ResolveAll<T>();

        foreach (var pool in pools)
        {
            _pools.Add(pool.GetItemType(), pool);
        }
    }

    public TObject Pop<TObject>()
    {
        return (TObject) _pools[typeof(TObject)].Pop();
    }

    public object Pop(Type type)
    {
        return _pools[type].Pop();
    }

    public void Push(object obj)
    {
        _pools[obj.GetType()].Push(obj);
    }
}
