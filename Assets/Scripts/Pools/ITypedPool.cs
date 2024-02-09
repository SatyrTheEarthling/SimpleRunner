using System;

/*
 * Extends IBasePool and provide Type of pool's Item.
 */
public interface ITypedPool : IBasePool
{
    Type GetItemType();
}
