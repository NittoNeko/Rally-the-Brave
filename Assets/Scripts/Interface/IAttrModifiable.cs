using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void AttrChange(EAttrType type, float previous, float current);

/// <summary>
/// Apply, store and calculate modifiers.
/// </summary>
public interface IAttrModifiable
{
    // triggered upon attribute changes
    event AttrChange OnAttrChange;

    /// <summary>
    /// Apply a modifier with/out refreshing attributes.
    /// </summary>
    /// <param name="attrModifier"></param>
    /// <param name="isRefresh"></param>
    void TakeModifier(EAttrType type, EAttrModLayer layer, float value, bool isRefresh = true);

    /// <summary>
    /// Remove a modifier with/out refreshing attributes.
    /// </summary>
    /// <param name="attrModifier"></param>
    /// <param name="isRefresh"></param>
    void RemoveModifier(EAttrType type, EAttrModLayer layer, float value, bool isRefresh = true);

    /// <summary>
    /// Refresh all or modifier-dirty only attributes at once.
    /// </summary>
    void RefreshAll(bool isDirtyOnly);

    /// <summary>
    /// Clear all attributes and modifications at once, be careful with it.
    /// </summary>
    void ResetAll();
}
