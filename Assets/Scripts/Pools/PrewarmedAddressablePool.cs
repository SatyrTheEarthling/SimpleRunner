using UnityEngine;

/*
 * Extends AddressablePool and provide Prewarm method. 
 * You can call it on game loading and some quantity of objects will be added to the pool.
 */
public abstract class PrewarmedAddressablePool<T> : AddressablePool<T> where T : MonoBehaviour, IPoolable
{
    public void Prewarm(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            var newOne = Instantiate();
            Push(newOne);
        }    
    }
}
