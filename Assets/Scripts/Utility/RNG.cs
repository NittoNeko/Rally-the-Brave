using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RNG
{
    // maximum 5 decimal digits will be saved
    private static readonly int Decimal = 100000;

    /// <summary>
    /// Given probability up to 5 decimal places, return true if hit, otherwise false.
    /// Probability is a percentage between 100 and 0.
    /// </summary>
    /// <param name="probality"></param>
    /// <returns></returns>
    public static bool IsHit(float percentage)
    {
        int _chance = Mathf.FloorToInt(percentage * Decimal);

        int _next = Random.Range(0, Decimal * 100);

        if (_next < _chance)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
