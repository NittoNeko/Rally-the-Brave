using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Apply, store and calculate modifiers.
/// </summary>
public interface IAttrModifierTaker
{
    /// <summary>
    /// Apply a modifier with/out refreshing attributes.
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="type"></param>
    /// <param name="value"></param>
    /// <param name="isRefresh"></param>
    void TakeModifier(float value, EAttrType type, EAttrModifierLayer layer, bool isRefresh = true);

    /// <summary>
    /// Remove a modifier with/out refreshing attributes.
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="type"></param>
    /// <param name="value"></param>
    /// <param name="isRefresh"></param>
    void RemoveModifier(float value, EAttrType type, EAttrModifierLayer layer, bool isRefresh = true);

    /// <summary>
    /// Refresh all(forced) or modifier-dirty only attributes at once.
    /// </summary>
    /// <param name="isDirtyOnly"></param>
    void RefreshAll(bool isForced = false);

    /// <summary>
    /// Clear all modifications of all attributes at once, be careful with it.
    /// </summary>
    void ResetAll();
}
