using System;
using System.Collections.Generic;

/*
 * Class provide functionailty to create and store Obstacle Pattern.
 * Obstacle is 6 cubes, 3 cubes wide and 2 cubes up. 
 * int Value stores the pattern by bits. Bit to cubes order: left to right, down to up.
 * Examples: 
 *   000 001 - only left cube.
 *   001 001 - two left cubes. One above another.
 *   100 100 - two right cubes. One above another.
 *   
 * Bonus stores type of bonus that will be added to this obstacle.
 * BonusPosition - position for bones. Coordinates same as cubes at Value. 
 * Examples: 
 *   010 000 - middle up.
 *   000 001 - down left.
 *   
 * To define a pattern, provide 3 methods: Add Left (Center, Right) Obstacle. 
 * If the same methods will be called second time - it will add second (up) cube. Third and every next time - no changes.
 */
public class ObstaclePattern
{
    public int Value { get; private set; }
    public Type Bonus { get; private set; }
    public int BonusPosition { get; private set; }

    public bool IsHasObstacle(int number)
    {
        return (Value & (1 << number)) > 0;
    }

    public void AddLeftObstacle()
    {
        AddObstacle(0);
    }

    public void AddCenterObstacle()
    {
        AddObstacle(1);
    }

    public void AddRightObstacle()
    {
        AddObstacle(2);
    }

    public int[] GetFreePositions()
    {
        List<int> result = new List<int>(6);
        for (int i = 0; i < 6; i++)
        {
            if (!IsHasObstacle(i))
                result.Add(i);
        }

        return result.ToArray();
    }

    public void AddBonus(Type bonus, int bonusPosition)
    {
        Bonus = bonus;
        BonusPosition = bonusPosition;
    }

    private void AddObstacle(int shift)
    {
        if (IsHasObstacle(shift))
        {
            Value |= 8 << shift;
        }
        else
        {
            Value |= 1 << shift;
        }
    }
}
