/*
 * Base Interface for Poolable object. It should support Reset functionality before cameback to the pool.
 */
public interface IPoolable
{
    public void Reset();
}
