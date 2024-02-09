using System;
using System.Collections.Generic;
using UnityEngine;

/*
 * Base Pool implementation for storing and instatiating GameObject with MonoBehaviour that implements IPoolable. 
 * Stores stack of objects.
 * All stored IPoolables has this pool as parent.
 * Pool is not active as GameObject.
 * You can take or instantiate new object using Pop, and then Push it back to the pool.
 * When object returns to pool, Reset will be called to it.
 */
public abstract class BasePool<T> : MonoBehaviour, IGenericBasePool<T> where T : MonoBehaviour, IPoolable
{
    private Stack<T> _content = new Stack<T>();

    protected abstract T Instantiate();

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public T Pop()
    {
        var result = TakeFromPool();
        return result;
    }

    private T TakeFromPool()
    {
        if (_content.Count > 0)
            return _content.Pop();

        return Instantiate();
    }

    public virtual void Push(T obj)
    {
        obj.Reset();
        obj.transform.SetParent(transform);

        _content.Push(obj);
    }

    object IBasePool.Pop()
    {
        return Pop();
    }

    public void Push(object obj)
    {
        Push((T)obj);
    }

    public Type GetItemType()
    {
        return typeof(T);
    }
}
