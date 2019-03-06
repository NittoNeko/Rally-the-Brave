using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Send modifiers to modifier takers.
/// </summary>
public interface IAttrModifier
{
    /// <summary>
    /// Apply all modifiers without clearing or refreshing.
    /// </summary>
    void ApplyAllModifiers();
}