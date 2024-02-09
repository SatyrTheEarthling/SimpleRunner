/*
 * Extends ITypedPool interface. As generic type has typed Pop and Push methods.
 */
public interface IGenericBasePool<T> : ITypedPool
{
    new T Pop();
    void Push(T obj);
}

