using System;
using UnityEngine;

/*
 * Settings of movement tweens.
 */
[Serializable]
public class MovementSettings
{
    public float OneRowMovementTimeout = 0.4f;
    public float TwoRowsMovementTimeout = 0.7f;

    public AnimationCurve OneRowEase;
    public AnimationCurve TwoRowsEase;

    public float JumpTimeout = 0.6f;
    public AnimationCurve JumpEase;

    public float FlyUpTimeout = 0.3f;
    public AnimationCurve FlyUpEase;

    public float FlyDownTimeout = 0.2f;
    public AnimationCurve FlyDownEase;
}
