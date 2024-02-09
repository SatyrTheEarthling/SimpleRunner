using System;
using UnityEngine;

/*
 * Core gameplay settings. 
 */
[Serializable]
public class CoreSettings
{
    public float Speed = 5f;
    [Tooltip("Compare with obstacles height, pls.")]
    public float JumpHeight = 4f;
    [Tooltip("Compare with obstacles height, pls.")]
    public float FlyHeight = 6f;

    [Tooltip("If Hero stands on max right postion his position.x will be equal to this value.")]
    public float HalfWidthOfTrack = 2f;
}
