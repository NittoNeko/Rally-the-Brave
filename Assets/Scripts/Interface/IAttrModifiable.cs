using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void AttrChange(EAttrType type, float previous, float current);

/// <summary>
/// Apply, store and calculate modifiers.
/// </summary>
public interface IAttrModifiable
{
    /// <summary>
    /// Triggered when a certain attribute has changed.
    /// </summary>
    event AttrChange OnAttrChange;

    /// <summary>
    /// Apply a modifier with/out refreshing attributes.
    /// </summary>
    /// <param name="attrModifier"></param>
    /// <param name="isRefresh"></param>
    void TakeModifier(AttrModifier attrModifier, bool isRefresh = true);

    /// <summary>
    /// Remove a modifier with/out refreshing attributes.
    /// </summary>
    /// <param name="attrModifier"></param>
    /// <param name="isRefresh"></param>
    void RemoveModifier(AttrModifier attrModifier, bool isRefresh = true);

    /// <summary>
    /// Apply modifiers with/out refreshing attributes.
    /// </summary>
    /// <param name="attrModifier"></param>
    /// <param name="isRefresh"></param>
    void TakeModifier(AttrModifier[] attrModifier, bool isRefresh = true);

    /// <summary>
    /// Remove modifiers with/out refreshing attributes.
    /// </summary>
    /// <param name="attrModifier"></param>
    /// <param name="isRefresh"></param>
    void RemoveModifier(AttrModifier[] attrModifier, bool isRefresh = true);

    /// <summary>
    /// Refresh all or modifier-dirty only attributes at once.
    /// </summary>
    void RefreshAll(bool isDirtyOnly);

    /// <summary>
    /// Clear all attributes and modifications at once, be careful with it.
    /// </summary>
    void ResetAll();
}
