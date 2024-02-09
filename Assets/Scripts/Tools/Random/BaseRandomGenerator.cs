using UnityEngine;

/*
 * Specific abstract class to implement specific probabilistic methods. 
 * Have abstract property Value, that used to all calculations.
 */
public abstract class BaseRandomGenerator
{
    public abstract float Value { get; }
    public abstract int CurrentIndex { get; }

    /// <summary>
    /// Return if this time luky on your side, with your chanse.
    /// </summary>
    /// <param name="chanse">0..1</param>
    /// <returns></returns>
    public bool IsPass(float chanse)
    {
        return Value <= chanse;
    }

    public void RollDices(int count, out int[] result, out int summ)
    {
        summ = 0;
        result = new int[count];

        for (int i = 0; i < count; i++)
        {
            summ += result[i] = RollDice();
        }
    }

    public int RollDice()
    {
        return Mathf.RoundToInt(Value * 6f + 0.5f);
    }

    public int ChooseOne(float[] weights)
    {        
        return ChooseOne(weights, weights.Length);
    }

    public int ChooseOne(float[] weights, int onlyFirstN)
    {
        var summ = 0f;
        for (int i = 0; i < onlyFirstN; i++)
        {
            summ += weights[i];
        }

        var result = Value * summ;
        var lastNotZero = 0;

        summ = 0f;
        for (int i = 0; i < onlyFirstN; i++)
        {
            if (summ <= result && summ + weights[i] > result)
                return i;

            summ += weights[i];

            if (weights[i] != 0)
                lastNotZero = i;
        }

        return lastNotZero;
    }

    public int Range(int min, int max)
    {
        // Форму усложнена так, чтобы вероятность выпадения каждого значения была равной. 
        // В более простой версии, веротяность выпадения min и max будет в два раза ниже, по сравнению с промежуточнымси значениями
        // Сравни (n; n+0.5) и (n + 0.5; n + 1.5)
        return min + Mathf.RoundToInt((max - min + 1) * Value - 0.5f);
    }

    public float RangeF(float min, float max)
    {
        return min + (max - min) * Value;
    }

    public int RangeFInt(float min, float max)
    {
        return Mathf.RoundToInt(RangeF(min, max));
    }
}
