/*
 * Base interface for every pool.
 */
public interface IBasePool
{
    object Pop();
    void Push(object obj);
}
