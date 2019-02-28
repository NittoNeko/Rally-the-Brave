using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Additive means the modifiers should add to base value
/// For example, increased by 10 and 20 will be
///   30 + 10 + 20 = 60
/// 
///   
/// Multiplicative means the modifier should multiply base values
/// We calculate the final modifiers layer by layer in order,
///   sum up additive modifiers first, then multiplicative modifiers.
/// For example, increased by 50% and 10 from Layer1, increased by 100% and 20 from Layer2
///   and increased by 150% from Layer1 will be
///   ((30 + 10) + (1 + 0.5 + 1.5) + 20) * (1 + 1) = 280
/// </summary>

public enum EAttrModifierType
{
    Additive, Multiplicative
}
