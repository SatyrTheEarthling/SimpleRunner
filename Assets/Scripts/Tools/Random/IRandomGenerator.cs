/*
 * Interface of IRandomGenerator. Provide access to some useful probabilistic methods.
 */
public interface IRandomGenerator
{
    float Value { get; }
    int CurrentIndex { get; }
    /// <param name="chanse">0..1</param>
    bool IsPass(float chanse);
    int RollDice();
    void RollDices(int count, out int[] result, out int summ);
    int ChooseOne(float[] weights);
    int Range(int min, int max);
    float RangeF(float min, float max);
    int RangeFInt(float min, float max);
}
