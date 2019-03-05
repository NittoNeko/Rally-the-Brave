using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttrModifier
{
    /// <summary>
    /// Apply all modifiers without clearing or refreshing.
    /// </summary>
    void ApplyAllModifiers();
}