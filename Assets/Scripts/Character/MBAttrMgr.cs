using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

/// <summary>
/// - Set, reset and refresh attributes.
/// - Apply attribute modifiers.
/// </summary>
public class MBAttrMgr : MonoBehaviour, IAttrModifierTaker, IAttrHolder
{
    // triggered when an attribute changes
    public event AttrChange OnAttrChange;

    // fixed base, maximum and minimum attributes
    [SerializeField]
    private SOAttrPresetTpl attrPreset;

    // attributes and their modifier
    private IAttribute[] attributes;

    private void Awake()
    {
        // initialize attribute containers
        attributes = new Attribute[EnumArray.AttrType.Length];
        for (int i = 0; i < attributes.Length; ++i)
        {
            EAttrType _type = (EAttrType)i;
            attributes[i] = FAttribute.Create(attrPreset.GetAttrPreset(_type), _type);
        }
       
        // get all attributes ready
        RefreshAll(true);
    }

    /// <summary>
    /// <see cref="IAttrHolder.GetAttr(EAttrType)"/>
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public float GetAttr(EAttrType type)
    {
        return attributes[(int)type].ActualValue;
    }

    /// <summary>
    /// <see cref="IAttrModifierTaker.TakeModifier(float, EAttrType, EAttrModifierLayer, bool)"/>
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="type"></param>
    /// <param name="value"></param>
    /// <param name="isRefresh"></param>
    public void TakeModifier(float value, EAttrType type, EAttrModifierLayer layer, bool isRefresh = true)
    {
        attributes[(int)type].TakeModifier(value, type, layer, isRefresh);

        if (isRefresh) RecalculateAttr(type);
    }

    /// <summary>
    /// <see cref="IAttrModifierTaker.RemoveModifier(float, EAttrType, EAttrModifierLayer, bool)"/>
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="type"></param>
    /// <param name="value"></param>
    /// <param name="isRefresh"></param>
    public void RemoveModifier(float value, EAttrType type, EAttrModifierLayer layer, bool isRefresh = true)
    {
        attributes[(int)type].RemoveModifier(value, type, layer, isRefresh);
    }

    /// <summary>
    /// <see cref="IAttrModifierTaker.RefreshAll(bool)"/>
    /// </summary>
    /// <param name="isDirtyOnly"
    public void RefreshAll(bool isForced = false)
    {
        for (int i = 0; i < attributes.Length; ++i)
        {
            RecalculateAttr((EAttrType)i, isForced);
        }
    }

    /// <summary>
    /// <see cref="IAttrModifierTaker.ResetAll"/>
    /// </summary>
    public void ResetAll()
    {
        for (int i = 0; i < attributes.Length; ++i)
        {
            attributes[i].Reset();
        }
    }

    /// <summary>
    /// Recalculate a certain attribute.
    /// </summary>
    /// <param name="type"></param>
    private void RecalculateAttr(EAttrType type, bool isForced = false)
    {
        float _previous = GetAttr(type);

        attributes[(int)type].Recalculate(isForced);

        // trigger attribute change event
        OnAttrChange?.Invoke(type, _previous, GetAttr(type));
    }
}

