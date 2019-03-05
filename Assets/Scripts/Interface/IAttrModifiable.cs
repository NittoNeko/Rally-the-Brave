using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttrModifiable
{
    void TakeModifier(AttrModifier attrModifier, bool isRefresh = true);

    void RemoveModifier(AttrModifier attrModifier, bool isRefresh = true);

    void TakeModifier(AttrModifier[] attrModifier, bool isRefresh = true);

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
