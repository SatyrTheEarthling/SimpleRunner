using System;
using UnityEngine;

/*
 * Mobile input settings.
 */
[Serializable]
public class SwipeInputSettings
{
    [Tooltip("Part of screen width 0..1")]
    public float LongMovementLength = 0.6f;
    [Tooltip("Part of screen width 0..1")]
    public float MinMovementLength = 0.1f;
}
