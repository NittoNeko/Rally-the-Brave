using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// Work with IAttrModifier on the same gameobject.
/// - Manipulate character attributes.
/// - Calculate attribute modifications.
/// </summary>
public class MBCharAttr : MonoBehaviour, IAttrHolder, IAttrModifiable
{
    // Fixed base, maximum and minimum attributes
    [SerializeField]
    private readonly SOCharAttr baseAttr;
    [SerializeField]
    private readonly SOCharAttr maxAttr;
    [SerializeField]
    private readonly SOCharAttr minAttr;

    // Attributes holder in enum order
    private readonly CharAttr[] actualAttr = InitializeCharAttr();

    public event AttrChange OnAttrChange;

    private static CharAttr[] InitializeCharAttr()
    {
        CharAttr[] _charAttrs = new CharAttr[EnumArray.AttrType.Length];
        for(int i = 0; i < _charAttrs.Length; ++i)
        {
            _charAttrs[i] = new CharAttr((EAttrType)i);
        }
        return _charAttrs;
    }

    public void TakeModifier(AttrModifier attrModifier, bool isRefresh = true)
    {
        ChangeModifiers(attrModifier, true);
        if (isRefresh) RecalculateAttr(attrModifier.AttrType);
    }

    public void RemoveModifier(AttrModifier attrModifier, bool isRefresh = true)
    {
        ChangeModifiers(attrModifier, false);
        if (isRefresh) RecalculateAttr(attrModifier.AttrType);
    }

    public void TakeModifier(AttrModifier[] attrModifier, bool isRefresh = true)
    {
        foreach(AttrModifier _modifier in attrModifier)
        {
            ChangeModifiers(_modifier, true);
        }
        if (isRefresh) RefreshAll(true);
    }

    public void RemoveModifier(AttrModifier[] attrModifier, bool isRefresh = true)
    {
        foreach (AttrModifier _modifier in attrModifier)
        {
            ChangeModifiers(_modifier, false);
        }
        if (isRefresh) RefreshAll(true);
    }

    public void RefreshAll(bool isDirtyOnly)
    {
        foreach (CharAttr _charAttr in actualAttr)
        {
            if (!isDirtyOnly ||_charAttr.IsModDirty == true)
            {
                RecalculateAttr(_charAttr.Type);
            }
        }
    }

    public void ResetAll()
    {
        foreach(CharAttr _charAttr in actualAttr)
        {
            _charAttr.Reset();
        }
    }

    /// <summary>
    /// Attribute getter.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public float GetAttr(EAttrType type)
    {
        return actualAttr[(int)type].Value;
    }

    private void Awake()
    {
        // Initialize all 
    }

    /// <summary>
    /// Attribute setter that does range checking and event handling.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="value"></param>
    private void SetAttr(EAttrType type, float value)
    {
        float _previous = GetAttr(type);
        float _upper = maxAttr.Get(type);
        float _lower = minAttr.Get(type);
        float _actual = Mathf.Max(Mathf.Min(value, _upper), _lower); // Range check

        actualAttr[(int)type].Value = _actual;

        // Trigger attribute change event
        OnAttrChange(type, _previous, _actual);
    }

    private void ChangeModifiers(AttrModifier attrModifier, bool isAdd)
    {
        CharAttr _charAttr = actualAttr[((int)attrModifier.AttrType)];

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

    // Recalculate attributes upon changes like wearing equipment
    private void RecalculateAttr(EAttrType type)
    {
        CharAttr _charAttr = actualAttr[(int)type];
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

