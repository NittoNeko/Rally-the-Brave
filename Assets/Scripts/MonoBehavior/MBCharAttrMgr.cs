using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// - Set, reset and refresh attributes.
/// - Apply attribute modifiers.
/// </summary>
public class MBCharAttrMgr : MonoBehaviour, IAttrModifiable
{
    public MBCharCont character;

    public event AttrChange OnAttrChange;

    /// <summary>
    /// <see cref="IAttrModifiable.TakeModifier(AttrModifier, bool)"/>
    /// </summary>
    /// <param name="attrModifier"></param>
    /// <param name="isRefresh"></param>
    public void TakeModifier(AttrModifier attrModifier, bool isRefresh = true)
    {
        ChangeModifiers(attrModifier, true);
        if (isRefresh) RecalculateAttr(attrModifier.AttrType);
    }

    /// <summary>
    /// <see cref="IAttrModifiable.RemoveModifier(AttrModifier, bool)"/>
    /// </summary>
    /// <param name="attrModifier"></param>
    /// <param name="isRefresh"></param>
    public void RemoveModifier(AttrModifier attrModifier, bool isRefresh = true)
    {
        ChangeModifiers(attrModifier, false);
        if (isRefresh) RecalculateAttr(attrModifier.AttrType);
    }

    /// <summary>
    /// <see cref="IAttrModifiable.TakeModifier(AttrModifier[], bool)"/>
    /// </summary>
    /// <param name="attrModifier"></param>
    /// <param name="isRefresh"></param>
    public void TakeModifier(AttrModifier[] attrModifier, bool isRefresh = true)
    {
        foreach(AttrModifier _modifier in attrModifier)
        {
            ChangeModifiers(_modifier, true);
        }
        if (isRefresh) RefreshAll(true);
    }

    /// <summary>
    /// <see cref="IAttrModifiable.RemoveModifier(AttrModifier[], bool)"/>
    /// </summary>
    /// <param name="attrModifier"></param>
    /// <param name="isRefresh"></param>
    public void RemoveModifier(AttrModifier[] attrModifier, bool isRefresh = true)
    {
        foreach (AttrModifier _modifier in attrModifier)
        {
            ChangeModifiers(_modifier, false);
        }
        if (isRefresh) RefreshAll(true);
    }

    /// <summary>
    /// <see cref="IAttrModifiable.RefreshAll(bool)"/>
    /// </summary>
    /// <param name="attrModifier"></param>
    /// <param name="isRefresh"></param>
    public void RefreshAll(bool isDirtyOnly)
    {
        foreach (CharAttr _charAttr in character.ActualAttr)
        {
            if (!isDirtyOnly ||_charAttr.IsModDirty == true)
            {
                RecalculateAttr(_charAttr.Type);
            }
        }
    }

    /// <summary>
    /// <see cref="IAttrModifiable.ResetAll"/>
    /// </summary>
    /// <param name="attrModifier"></param>
    /// <param name="isRefresh"></param>
    public void ResetAll()
    {
        foreach(CharAttr _charAttr in character.ActualAttr)
        {
            _charAttr.Reset();
        }
    }

    /// <summary>
    /// Attribute setter that does range checking and event handling.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="value"></param>
    private void SetAttr(EAttrType type, float value)
    {
        float _previous = character.GetAttr(type);
        float _upper = character.MaxAttr.Get(type);
        float _lower = character.MinAttr.Get(type);
        float _actual = Mathf.Max(Mathf.Min(value, _upper), _lower); // Range check

        character.ActualAttr[(int)type].Value = _actual;

        // Trigger attribute change event
        OnAttrChange(type, _previous, _actual);
    }

    /// <summary>
    /// Add or remove modifier.
    /// </summary>
    /// <param name="attrModifier"></param>
    /// <param name="isAdd"></param>
    private void ChangeModifiers(AttrModifier attrModifier, bool isAdd)
    {
        CharAttr _charAttr = character.ActualAttr[((int)attrModifier.AttrType)];

        // As for independent, we should keep its track first
        //   then recalculate its caches.
        if (attrModifier.Layer == (int)EAttrModLayer.Independent)
        {
            if (isAdd)
            {
                _charAttr.Independent.Add(attrModifier.Value);
            }
            else
            {
                _charAttr.Independent.Remove(attrModifier.Value);
            }

            // Recalculate caches
            float _result = 1;
            foreach (float _value in _charAttr.Independent)
            {
                _result *= _value;
            }
            _charAttr.Modifiers[(int)attrModifier.Layer] = _result;
        }
        else
        {
            _charAttr.Modifiers[(int)attrModifier.Layer] += isAdd ? attrModifier.Value : -attrModifier.Value;
        }

        // Set dirty
        _charAttr.IsModDirty = true;
    }

    /// <summary>
    /// Recalculate a certain attribute.
    /// </summary>
    /// <param name="type"></param>
    private void RecalculateAttr(EAttrType type)
    {
        CharAttr _charAttr = character.ActualAttr[(int)type];
        float _additive = _charAttr.Modifiers[(int)EAttrModLayer.Additive];
        float _multiplicative = 1;

        // Calculate final multiplicative modification
        for (int i = 0; i < _charAttr.Modifiers.Length; ++i)
        {
            if (i != (int)EAttrModLayer.Additive)
            {
                _multiplicative *= _charAttr.Modifiers[i];
            }
        }

        float result = (_charAttr.Value + _additive) * _multiplicative;

        // Reset dirty flag
        _charAttr.IsModDirty = false;

        SetAttr(type, result);
    }
}

