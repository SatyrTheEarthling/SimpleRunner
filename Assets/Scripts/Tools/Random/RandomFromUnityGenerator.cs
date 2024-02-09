using UnityEngine;

/*
 * Implementation of BaseRandomGenerator that use UnityEngine.Random.value to gain random value.
 */
public class RandomFromUnityGenerator: BaseRandomGenerator, IRandomGenerator
{
    public override float Value => Random.value;
    public override int CurrentIndex => throw new System.NotSupportedException();
}
