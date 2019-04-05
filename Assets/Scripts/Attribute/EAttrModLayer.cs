using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Modifier layers include multiplicative, additive and independent.
/// </summary>
public enum EAttrModLayer
{
    // independent is a special multiplicative that only timeses other multiplicative
    Independent, 
    Additive,
    // Mult1 applies to Equipment
    Mult1,
    // Mult2 applies to Status
    Mult2,
    // Mult3 applies to Souls
    Mult3
}
