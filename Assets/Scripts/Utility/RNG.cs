using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RNG
{
    // maximum 7 decimal digits will be saved
    private static readonly int Decimal = 10000000;

    /// <summary>
    /// Given probability up to 7 decimal places, return true if hit, otherwise false.
    /// Probability is a decimal between 1 and 0.
    /// </summary>
    /// <param name="probality"></param>
    /// <returns></returns>
    public static bool IsHit(float probability)
    {
        int _chance = Mathf.RoundToInt(probability * Decimal);

        int _next = Random.Range(0, Decimal);

        if (_next < _chance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
