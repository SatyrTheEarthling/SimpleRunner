using System;

/*
 * In-game bonus settings. Weights (chances) to meet in game.
 */
[Serializable]
public class BonusesSettings
{
    public float CoinChanceWeight = 1.4f;
    public float FlyChanceWeight = 0.1f;
    public float SlowDownChanceWeight = 0.4f;
    public float SpeedUpChanceWeight = 0.5f;
}